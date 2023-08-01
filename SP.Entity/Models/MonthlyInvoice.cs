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
        public virtual User User { get; set; }


    }
    public class MonthlyInvoiceConfiguration : IEntityTypeConfiguration<MonthlyInvoice>
    {
        public void Configure(EntityTypeBuilder<MonthlyInvoice> builder)
        {
            builder.Property(x => x.Date).IsRequired();

          
        }
    }
}

//KENDİME NOT Şu anki tarihi alın. ''mailjob''
//MonthlyInvoice koleksiyonundaki her bir faturayı kontrol edin ve ödeme tarihini alın.
//Şu anki tarihten ödeme tarihini çıkartarak gün sayısını hesaplayın.
