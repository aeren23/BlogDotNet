using BlogDotNet.EntityLayer.DTOs.Users;
using BlogDotNet.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.ServiceLayer.Services.Abstractions
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUserWithRoleAsync();
        Task<List<AppRole>> GetAllRolesAsync();
        Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto);
        Task<AppUser> GetAppUserByIdAsync(Guid id);
        Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<string> GetUserRoleAsync(AppUser user);
        Task<IdentityResult> DeleteAsync(Guid userId);
        Task<UserProfileDto> GetUserProfileAsync();
        Task<bool> UserProfileUpdateAsync(UserProfileDto userProfileDto);
    }
}
