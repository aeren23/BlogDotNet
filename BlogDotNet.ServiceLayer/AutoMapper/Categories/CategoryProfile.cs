using AutoMapper;
using BlogDotNet.EntityLayer.DTOs.Categories;
using BlogDotNet.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.ServiceLayer.AutoMapper.Categories
{
    public class CategoryProfile: Profile
    {
        public CategoryProfile() 
        {
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryAddDto, Category>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();
        }

    }
}
