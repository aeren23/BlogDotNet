using BlogDotNet.CoreLayer.Entities;
using BlogDotNet.EntityLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.EntityLayer.Entities
{
    public class Image:EntityBase
    {
        public Image() 
        {

        }

        public Image(string fileName,string fileType,string createdBy)
        {
            FileName = fileName;
            FileType = fileType;
            CreatedBy = createdBy;
        }

        public string FileName { get; set; }
        public string FileType { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<AppUser> AppUsers { get; set; }
    }
}
