using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Patika.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Entity.Models;

namespace SP.Entity
{
    public class MonthlyInvoice
    {
        public int MonthlyInvoiceId { get; set; }
        public decimal DuesAmount { get; set; }
        public decimal WaterBill { get; set; }
        public decimal ElectricityBill { get; set; }
        public decimal GasBill { get; set; }
        public DateTime Date { get; set; }

        public int ApartmentId { get; set; }
        public virtual Apartment Apartment { get; set; }


    }
    public class MonthlyInvoiceConfiguration : IEntityTypeConfiguration<MonthlyInvoice>
    {
        public void Configure(EntityTypeBuilder<MonthlyInvoice> builder)
        {
            builder.HasKey(mi => mi.MonthlyInvoiceId);
            builder.Property(mi => mi.DuesAmount).IsRequired();

            // WaterBill alanı için veritabanında uygun sütun tipini belirtmek
            builder.Property(mi => mi.WaterBill)
                   .HasColumnType("decimal(18, 2)")
                   .IsRequired();

            // ElectricityBill alanı için veritabanında uygun sütun tipini belirtmek
            builder.Property(mi => mi.ElectricityBill)
                   .HasColumnType("decimal(18, 2)")
                   .IsRequired();

            // GasBill alanı için veritabanında uygun sütun tipini belirtmek
            builder.Property(mi => mi.GasBill)
                   .HasColumnType("decimal(18, 2)")
                   .IsRequired();

            builder.Property(mi => mi.Date).IsRequired();

            // MonthlyInvoice ile Apartment ilişkisi
            builder.HasOne(mi => mi.Apartment)
                   .WithMany(a => a.MonthlyInvoices)
                   .HasForeignKey(mi => mi.ApartmentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }
    }

//KENDİME NOT Şu anki tarihi alın. ''mailjob''
//MonthlyInvoice koleksiyonundaki her bir faturayı kontrol edin ve ödeme tarihini alın.
//Şu anki tarihten ödeme tarihini çıkartarak gün sayısını hesaplayın.
