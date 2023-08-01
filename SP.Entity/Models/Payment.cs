using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Entity;
using SP.Entity.Models;

namespace Patika.Entity.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CVV { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.PaymentId);
            builder.Property(p => p.CreditCardNumber).IsRequired();
            builder.Property(p => p.ExpiryDate).IsRequired();
            builder.Property(p => p.CVV).IsRequired();
            builder.Property(p => p.Amount).IsRequired();
            builder.Property(p => p.PaymentDate).IsRequired();

            builder.Property(p => p.Amount)
              .HasColumnType("decimal(18, 2)")
              .IsRequired();

            builder.Property(p => p.PaymentDate).IsRequired();

            // Payment ile User ilişkisi
            builder.HasOne(p => p.User)
                   .WithMany(u => u.Payments)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);



        }
    }

}
