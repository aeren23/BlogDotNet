using BlogDotNet.EntityLayer.DTOs.Categories;
using BlogDotNet.EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.EntityLayer.DTOs.Articles
{
    public class ArticleUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Image Image { get; set; }
        public IFormFile? Photo { get; set; }
        public Guid CategoryId { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}
