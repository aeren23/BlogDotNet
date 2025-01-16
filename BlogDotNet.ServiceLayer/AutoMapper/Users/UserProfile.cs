using AutoMapper;
using BlogDotNet.EntityLayer.DTOs.Users;
using BlogDotNet.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.ServiceLayer.AutoMapper.Users
{
    public class UserProfile:Profile
    {
        public UserProfile() 
        {
            CreateMap<UserDto, AppUser>().ReverseMap();
            CreateMap<UserAddDto, AppUser>().ReverseMap();
            CreateMap<UserUpdateDto, AppUser>().ReverseMap();
            CreateMap<UserProfileDto, AppUser>().ReverseMap();
        }
    }
}
