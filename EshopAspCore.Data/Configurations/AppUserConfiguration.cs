using EshopAspCore.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");

            builder.Property(a => a.FirstName).HasMaxLength(200).IsRequired();

            builder.Property(a => a.LastName).HasMaxLength(200).IsRequired();

            builder.Property(a => a.Dob).IsRequired();
        }
    }
}
