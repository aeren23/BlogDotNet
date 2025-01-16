using BlogDotNet.CoreLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.EntityLayer.Entities
{
    public class AppUser : IdentityUser<Guid>, IEntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid ImageId { get; set; } = Guid.Parse("F388B35C-1577-4578-B5FF-571750064DF3");
        public Image Image { get; set; }
        public ICollection<Article> Articles { get; set; }  
    }
}
