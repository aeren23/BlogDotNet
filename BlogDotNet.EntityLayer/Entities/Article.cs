using BlogDotNet.CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.EntityLayer.Entities
{
    public class Article:EntityBase
    {

        public Article()
        {

        }
        public Article(string title, string content, Guid userId, Guid categoryId,string createdByEmail)
        {
            Title = title;
            Content = content;
            UserId = userId;
            CategoryId = categoryId;           
            CreatedBy = createdByEmail;
        }
        public Article(string title,string content,Guid userId,Guid categoryId,Guid imageId, string createdByEmail)
        {
            Title = title;
            Content = content;
            UserId = userId;
            CategoryId = categoryId;
            ImageId = imageId;
            CreatedBy = createdByEmail;
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; } = 0;
        public string Author { get; set; } = "Mr.Nobody";

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid? ImageId { get; set; } 
        public Image Image { get; set; }

        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<ArticleVisitor> ArticleVisitors { get; set; }
    }
}
