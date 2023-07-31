
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SP.Entity.Models;
using System.Data;
using Patika.Entity.Models;

namespace SP.Entity
{
    public class User 
    {
        public string UserId { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastActivity { get; set; }
        public int PasswordRetryCount { get; set; }
        public int Status { get; set; }
        public ICollection<Apartment> Apartments { get; set; } // Kullanıcının birden fazla daireye sahip olması
        public ICollection<MonthlyInvoice> MonthlyInvoices { get; set; } //Dairede oturanların birden fazla aiat ödemesi olacak sonuçta
        public ICollection<Payment> Payments { get; set; }
        //MESAJLAR
        public ICollection<Messages> SentMessages { get; set; } // Kullanıcının gönderdiği mesajlar
    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.UserId).IsRequired().ValueGeneratedNever();
            builder.HasKey(x => x.UserId);

            builder.HasMany(x => x.Apartments)
                   .WithOne(x => x.Users)
                   .HasForeignKey(x => x.Users)
                   .IsRequired(true);

            builder.HasMany(x => x.MonthlyInvoices)
                   .WithOne(x => x.Users)
                   .HasForeignKey(x => x.UserId)
                   .IsRequired(true);

            builder.HasMany(x => x.Payments)
                   .WithOne(x => x.Users)
                   .HasForeignKey(x => x.UserId)
                   .IsRequired(true);

            builder.HasMany(x => x.SentMessages)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .IsRequired(true);
        }
    }
}

