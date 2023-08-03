using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Entity;
using SP.Entity.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patika.Entity.Models
{
    public class Payment
    {
        public int Id { get; set; } // Ödeme işlemi için ID veya benzersiz kimlik alanı
        public DateTime PaymentDate { get; set; } // Ödeme tarihi
        public decimal InvoiceAmount { get; set; } // Ödeme miktarı
        public int UserId { get; set; } // Ödeme yapan kullanıcının ID'si
      
        public virtual User User { get; set; } // Kullanıcı ile ilişki için dış anahtar

        public int MonthlyInvoiceId { get; set; }

 
        public virtual MonthlyInvoice MonthlyInvoice { get; set; }

        public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
        {
            public void Configure(EntityTypeBuilder<Payment> builder)
            {
                // ...
                builder.HasOne(p => p.MonthlyInvoice)
                    .WithMany(mi => mi.Payments)
                    .HasForeignKey(p => p.MonthlyInvoiceId)
                    .OnDelete(DeleteBehavior.Restrict);
                // ...
            }
        }

    }
}

