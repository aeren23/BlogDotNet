using AutoMapper;
using BlogDotNet.DataAccessLayer.UnitOfWorks;
using BlogDotNet.EntityLayer.DTOs.Users;
using BlogDotNet.EntityLayer.Entities;
using BlogDotNet.EntityLayer.Entities.Enums;
using BlogDotNet.ServiceLayer.Extensions;
using BlogDotNet.ServiceLayer.Helpers.Images;
using BlogDotNet.ServiceLayer.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.ServiceLayer.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IImageHelper imageHelper;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal _user;

        public UserService(IUnitOfWork unitOfWork,SignInManager<AppUser> signInManager,IImageHelper imageHelper,IHttpContextAccessor httpContextAccessor, IMapper mapper,UserManager<AppUser> userManager,RoleManager<AppRole> roleManager) 
        {
            this.unitOfWork = unitOfWork;
            this.signInManager = signInManager;
            this.imageHelper = imageHelper;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
        }

        public async Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto)
        {
            var map = mapper.Map<AppUser>(userAddDto);
            map.UserName = userAddDto.Email;
            var result = await userManager.CreateAsync(map, userAddDto.Password);
            if (result.Succeeded) 
            {
                var findRole = await roleManager.FindByIdAsync(userAddDto.RoleId.ToString());
                await userManager.AddToRoleAsync(map, findRole.ToString());
                return result;
            }
            else
            {
                return result;
            }
        }

        public async Task<IdentityResult> DeleteAsync(Guid userId)
        {
            var user = await GetAppUserByIdAsync(userId);
            return await userManager.DeleteAsync(user);
        }

        public async Task<List<AppRole>> GetAllRolesAsync()
        {
            return await roleManager.Roles.ToListAsync();
        }

        public async Task<List<UserDto>> GetAllUserWithRoleAsync()
        {
            var users = await userManager.Users.ToListAsync();
            var map = mapper.Map<List<UserDto>>(users);

            foreach (var user in map)
            {
                var findUser = await userManager.FindByIdAsync(user.Id.ToString());
                var role = string.Join("", await userManager.GetRolesAsync(findUser));

                user.Role = role;
            }
            
            return map;
        }

        public async Task<AppUser> GetAppUserByIdAsync(Guid userId)
        {
            var userRepository = unitOfWork.GetRepository<AppUser>();
            var user = await userRepository.GetAsync(
                u => u.Id == userId,  // Kullanıcıyı ID'ye göre filtrele
                u => u.Image           // Image ilişkilendirmesini dahil et
            );
            return user;
            //return await userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<string> GetUserRoleAsync(AppUser user)
        {
            return string.Join("", await userManager.GetRolesAsync(user));
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var user = await GetAppUserByIdAsync(userUpdateDto.Id);
            var userRole = await GetUserRoleAsync(user);
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {

                await userManager.RemoveFromRoleAsync(user, userRole);

                var findRole = await roleManager.FindByIdAsync(userUpdateDto.RoleId.ToString());
                await userManager.AddToRoleAsync(user, findRole.Name);
                return result;
            }
            return result;
        }

        public async Task<UserProfileDto> GetUserProfileAsync()
        {
            var user = await userManager.GetUserAsync(_user);
            var getImage = await unitOfWork.GetRepository<AppUser>().GetAsync(x => x.Id == user.Id,x=>x.Image);
            var mappedData = mapper.Map<UserProfileDto>(user);
            mappedData.Image.FileName = getImage.Image.FileName;
            return mappedData;
        }

        private async Task<Guid> UploadImageForUser(UserProfileDto userProfileDto)
        {
            var userEmail=_user.GetLoggedInEmail();

            var imageUpload=await imageHelper.Upload($"{userProfileDto.FirstName}{userProfileDto.LastName}",userProfileDto.Photo,ImageType.User);
            Image image = new(imageUpload.FullName, userProfileDto.Photo.ContentType, userEmail);
            await unitOfWork.GetRepository<Image>().AddAsync(image);

            return image.Id;
        }

        public async Task<bool> UserProfileUpdateAsync(UserProfileDto userProfileDto)
        {
            var userId = _user.GetLoggedInUserId();
            var user = await GetAppUserByIdAsync(userId);

            var isVerified = await userManager.CheckPasswordAsync(user, userProfileDto.CurrentPassword);
            if (isVerified && userProfileDto.NewPassword != null)
            {
                var result = await userManager.ChangePasswordAsync(user, userProfileDto.CurrentPassword, userProfileDto.NewPassword);
                if (result.Succeeded)
                {
                    await userManager.UpdateSecurityStampAsync(user);
                    await signInManager.SignOutAsync();
                    await signInManager.PasswordSignInAsync(user, userProfileDto.NewPassword, true, false);

                    mapper.Map(userProfileDto, user);

                    if (userProfileDto.Photo != null)
                        user.ImageId = await UploadImageForUser(userProfileDto);

                    await userManager.UpdateAsync(user);
                    await unitOfWork.SaveAsync();

                    return true;
                }
                else
                    return false;
            }
            else if (isVerified)
            {
                await userManager.UpdateSecurityStampAsync(user);
                //Profil Fotosu güncellenmeden çıkış yapması ve güncelleme hatası vermemesi için kullanılabilir
                //await signInManager.SignOutAsync();
                //await signInManager.PasswordSignInAsync(user, userProfileDto.NewPassword, true, false);
                var existingImageId = user.ImageId; // Mevcut ImageId'yi kaydet
                mapper.Map(userProfileDto, user);
                user.ImageId = existingImageId; // ImageId'yi eski değerine geri getir

                if (userProfileDto.Photo != null)
                    user.ImageId = await UploadImageForUser(userProfileDto);

                await userManager.UpdateAsync(user);
                await unitOfWork.SaveAsync();
                return true;
            }
            else
                return false;
        }
    }
}
