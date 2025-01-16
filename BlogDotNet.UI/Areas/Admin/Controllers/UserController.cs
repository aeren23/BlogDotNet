using AutoMapper;
using BlogDotNet.EntityLayer.DTOs.Articles;
using BlogDotNet.EntityLayer.DTOs.Users;
using BlogDotNet.EntityLayer.Entities;
using BlogDotNet.ServiceLayer.Services.Abstractions;
using BlogDotNet.UI.ResultMessages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace BlogDotNet.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController: Controller
    {
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;
        private readonly IUserService userService;

        public UserController(IUserService userService,IMapper mapper, IToastNotification toastNotification ) 
        {
            this.mapper = mapper;
            this.toastNotification = toastNotification;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users =await userService.GetAllUserWithRoleAsync();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var roles = await userService.GetAllRolesAsync();
            return View(new UserAddDto { Roles = roles });
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            var map=mapper.Map<AppUser>(userAddDto);
            var roles = await userService.GetAllRolesAsync();
            if (ModelState.IsValid)
            {
                map.UserName = userAddDto.Email;
                var result = await userService.CreateUserAsync(userAddDto);
                if (result.Succeeded)
                {
                    toastNotification.AddSuccessToastMessage(Messages.User.Add(userAddDto.Email), new ToastrOptions { Title = "Başarılı!" });
                    return RedirectToAction("Index", "User", new { Area = "Admin" });
                }
                else
                {
                    foreach (var errors in result.Errors)
                    {
                        ModelState.AddModelError("", errors.Description);
                    }
                    return View(new UserAddDto { Roles = roles });
                }

            }
            return View(new UserAddDto { Roles = roles });
        }

        [HttpGet]
        public async Task<IActionResult> Update (Guid userId)
        {
            var user=await userService.GetAppUserByIdAsync(userId);

            var roles= await userService.GetAllRolesAsync();

            var mappedData=mapper.Map<UserUpdateDto>(user);
            mappedData.Roles = roles;
            return View(mappedData);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            var user = await userService.GetAppUserByIdAsync(userUpdateDto.Id);
            if (user != null)
            {
                var roles = await userService.GetAllRolesAsync();
                if (ModelState.IsValid)
                {
                    var map=mapper.Map(userUpdateDto, user);
                    user.UserName = userUpdateDto.Email;
                    user.SecurityStamp=Guid.NewGuid().ToString();
                    var result = await userService.UpdateUserAsync(userUpdateDto); 
                    if (result.Succeeded) 
                    {
                        toastNotification.AddSuccessToastMessage(Messages.User.Update(userUpdateDto.Email), new ToastrOptions { Title = "Başarılı!" });
                        return RedirectToAction("Index", "User", new { Area = "Admin" });
                    }
                    else
                    {
                        foreach(var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);

                        }
                        return View(new UserUpdateDto { Roles=roles});
                    }
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Delete(Guid userId)
        {
            var user=await userService.GetAppUserByIdAsync(userId);
            var result = await userService.DeleteAsync(userId);
            if (result.Succeeded) { 
                toastNotification.AddSuccessToastMessage(Messages.User.Delete(user.Email), new ToastrOptions { Title = "Başarılı!" });
                return RedirectToAction("Index", "User", new { Area = "Admin" });
            }
            else
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("",error.Description);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var mappedData =await userService.GetUserProfileAsync();
            return View(mappedData);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserProfileDto userProfileDto)
        {
            if(ModelState.IsValid)
            {
                var result=await userService.UserProfileUpdateAsync(userProfileDto);
                if (result)
                {
                    toastNotification.AddSuccessToastMessage("Profil Güncelleme İşlemi Başarılı", new ToastrOptions { Title = "Başarılı!" });
                    return RedirectToAction("Index", "User", new { Area = "Admin" });
                }
                else
                {
                    var profile = await userService.GetUserProfileAsync();
                    toastNotification.AddErrorToastMessage("Profil Güncelleme İşlemi Başarısız", new ToastrOptions { Title = "Başarısız!" });
                    return View(profile);
                }
            }
            else
                return NotFound ();
        }
    }
}
