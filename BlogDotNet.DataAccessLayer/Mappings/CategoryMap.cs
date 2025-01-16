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
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category
            {
                Id = Guid.Parse("A9CDF98C-C8BA-4045-92F6-A42AE9213062"),
                Name = "ASP.Net Core",
                CreatedBy = "Admintest",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            });
        }
    }
}
