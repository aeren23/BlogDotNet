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
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasData(new Article
            {
                Id=Guid.NewGuid(),
                Title="ASP.Net Core",
                Content= "ASP.Net Core Lorem ipsum dolor sit amet consectetur adipisicing elit. Provident dolorum tempore enim itaque libero veritatis architecto similique aperiam perspiciatis voluptatum, omnis, repellendus vitae odio laudantium? Dolore cumque laudantium libero ipsum\r\nLorem ipsum dolor sit amet consectetur, adipisicing elit. Nulla non dolorem laborum eligendi similique quibusdam, facere, nisi minus vel officia hic, mollitia id nemo neque adipisci accusamus rerum voluptas amet suscipit maxime aspernatur. Suscipit.",
                ViewCount=0,
                Author="Admin",
                CreatedBy="Admintest",
                CreatedDate=DateTime.Now,
                CategoryId= Guid.Parse("A9CDF98C-C8BA-4045-92F6-A42AE9213062"),
                ImageId=Guid.Parse("CC629C13-8298-4C24-B2AB-7BF568A23814"),
                IsDeleted=false,
                UserId= Guid.Parse("25C46538-FFD1-48F6-A273-2E2937D32A32")
            });
        }
    }
}
