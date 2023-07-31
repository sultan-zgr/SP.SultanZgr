using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Entity.Models
{
    public class Bank
    {
        public int BankId { get; set; }
      
        public string CreditCardNumber { get; set; } // Kullanıcının kredi kartı numarası
        public string ExpiryDate { get; set; } // Kredi kartının son kullanma tarihi (MM/YY formatında)
        public string CVV { get; set; } // Kredi kartının güvenlik kodu
        public decimal Amount { get; set; } // Ödeme miktarı
    }
    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {

            // Anahtar alanı (Primary Key)
            builder.HasKey(b => b.BankId);

            // Kredi kartı numarası alanı
            builder.Property(b => b.CreditCardNumber)
                   .IsRequired()
                   .HasMaxLength(16); // Kredi kartı numarası 16 haneli olmalı

            // Son kullanma tarihi alanı
            builder.Property(b => b.ExpiryDate)
                   .IsRequired()
                   .HasMaxLength(5); // Son kullanma tarihi MM/YY formatında olmalı

            // CVV alanı
            builder.Property(b => b.CVV)
                   .IsRequired()
                   .HasMaxLength(3); // CVV 3 haneli olmalı

            // Ödeme miktarı alanı
            builder.Property(b => b.Amount)
                   .IsRequired();
        }
    }
}
