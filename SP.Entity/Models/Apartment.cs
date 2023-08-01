using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SP.Entity.Models;

namespace SP.Entity
{
    public class Apartment
    {
        public int ApartmentId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public bool IsOccupied { get; set; } //Oturan biri var mı
        public bool IsOwner { get; set; } // Sahibi mi Kiracı mı
        public string Type { get; set; }  // 2+1 3+1 vs..   
        public string BlockName { get; set; }
        public int FloorNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public virtual ICollection<MonthlyInvoice> MonthlyInvoices { get; set; }
    }
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.Property(x => x.FloorNumber).IsRequired();
            builder.Property(x => x.ApartmentNumber).IsRequired();
            builder.Property(x => x.Type).HasMaxLength(50).IsRequired();
            builder.Property(x => x.BlockName).HasMaxLength(50).IsRequired();

            // IsOccupied ilişkisi
            builder.Property(x => x.IsOccupied).IsRequired();

            // IsOwner ilişkisi
            builder.Property(x => x.IsOwner).IsRequired();

            

        }
    }

}

