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
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(new AppUserRole
            {
                UserId= Guid.Parse("25C46538-FFD1-48F6-A273-2E2937D32A32"),
                RoleId = Guid.Parse("3A86CD3A-A3EA-4F84-823C-079A801F3D19")
            },
            new AppUserRole
            {
                UserId= Guid.Parse("EFF33B70-E4D2-48BD-8EC8-58B7185A8E68"),
                RoleId= Guid.Parse("41DDCC0B-6EB8-42B8-9180-0C0F4E79367D")
            });
        }
    }
}
