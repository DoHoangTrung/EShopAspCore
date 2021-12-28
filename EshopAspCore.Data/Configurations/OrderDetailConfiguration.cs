using EshopAspCore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetail");

            builder.HasKey(o => o.OrderId);

            builder.HasOne(od => od.Order).WithMany(od => od.OrderDetails).HasForeignKey(od => od.OrderId);

            builder.HasOne(o => o.Product).WithMany(o => o.OrderDetails).HasForeignKey(o => o.OrderId);
        }
    }
}
