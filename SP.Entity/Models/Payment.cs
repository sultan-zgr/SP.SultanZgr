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
        public Bank Bank { get; set; } // Ödeme için kullanılan banka bilgileri
        public virtual User User { get; set; }

    }
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(x => x.CreditCardNumber).IsRequired();
            builder.Property(x => x.ExpiryDate).IsRequired();
            builder.Property(x => x.CVV).IsRequired();

         
        }
    }

}
