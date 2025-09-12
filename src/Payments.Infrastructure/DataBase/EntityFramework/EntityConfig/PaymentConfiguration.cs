using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payments.Infrastructure.DataBase.EntityFramework.EntityConfig
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
              .ValueGeneratedOnAdd();

            builder.Property(u => u.UserId);

            builder.Property(u => u.PaymentDate);

            builder.Property(g => g.Amount)
            .HasColumnType("decimal(10,2)");

            builder.Property(u => u.Currency)
               .HasMaxLength(5);

            builder.Property(u => u.Description)
               .HasMaxLength(100);

            builder.Property(u => u.CreatedAt).ValueGeneratedOnAdd();

            builder.Property(u => u.UpdatedAt).ValueGeneratedOnAddOrUpdate();

            builder.ToTable("payment");
        }
    }
}
