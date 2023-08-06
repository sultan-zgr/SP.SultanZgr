﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Patika.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection.Emit;

namespace SP.Entity.Models
{

    public class User
    {
        public int UserId { get; set; }
        public decimal Balance { get; set; }
        public string Role { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PasswordRetryCount { get; set; }
        public int Status { get; set; }
        public string? TCNo { get; set; }
        public string? VehiclePlateNumber { get; set; }

        // Diğer özel alanlarınızı da ekleyebilirsiniz.

        // İlişkiler
        public List<Messages> MessagesSending { get; set; } // Kullanıcının gönderdiği mesajlar
      
        public List<Payment> Payments { get; set; } // Kullanıcının yaptığı ödemeler
        public bool IsPayment  { get; set; }
        public List<Apartment> Apartments { get; set; } // Kullanıcının sahip olduğu daireler
    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Tablo adı ve anahtar belirleme
            builder.ToTable("Users");
            builder.HasKey(x => x.UserId);

            // Sütun özellikleri ve ilişkilerin belirlenmesi
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(100);
            builder.Property(x => x.PasswordRetryCount).IsRequired();
            builder.Property(x => x.Status).IsRequired().HasMaxLength(20);
            builder.Property(x => x.TCNo).IsRequired().HasMaxLength(11);
            builder.Property(x => x.VehiclePlateNumber).HasMaxLength(20);

            // İlişkiler
            builder.HasMany(x => x.MessagesSending).WithOne().HasForeignKey("SenderId").OnDelete(DeleteBehavior.NoAction);
            //builder.HasMany(x => x.MessagesReceivng).WithOne().HasForeignKey("ReceiverId").OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Payments).WithOne().HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Apartments).WithOne().HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict);

            // User ile UserRole arasındaki ilişkiyi belirleme
            builder.HasOne(x => x.Role).WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.Restrict);

            builder.Property(u => u.Balance)
             .HasColumnType("decimal(18, 2)");

            builder.Property(p=> p.Payments)
        .HasColumnType("decimal(18, 2)");
        }
    }
}


