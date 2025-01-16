using BlogDotNet.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.DataAccessLayer.Mappings
{
    public class ImageMap : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasData(new Image
            {
                Id= Guid.Parse("CC629C13-8298-4C24-B2AB-7BF568A23814"),
                FileName="images/testimages",
                FileType="png",
                CreatedBy="Admintest",
                CreatedDate=DateTime.Now,
                IsDeleted=false
            });
        }
    }
}
