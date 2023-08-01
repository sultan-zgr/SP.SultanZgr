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
        public bool IsOccupied { get; set; }
        public bool IsOwner { get; set; }
        public string Type { get; set; }
        public string BlockName { get; set; }
        public int FloorNumber { get; set; }
        public int ApartmentNumber { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<MonthlyInvoice> MonthlyInvoices { get; set; }
    }
    public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
    {
        public void Configure(EntityTypeBuilder<Apartment> builder)
        {
            builder.HasKey(a => a.ApartmentId);
            builder.Property(a => a.BlockName).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Type).HasMaxLength(50).IsRequired();
            builder.Property(a => a.FloorNumber).IsRequired();
            builder.Property(a => a.ApartmentNumber).IsRequired();
            builder.Property(a => a.IsOccupied).IsRequired();
            builder.Property(a => a.IsOwner).IsRequired();

            // Apartment ile User ilişkisi
            builder.HasOne(a => a.User)
                   .WithMany(u => u.Apartments)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);


        }
    }

}

