using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Entity.Models
{
    public class Admin 
    {
        public int AdminId { get; set; }
        public string Role { get; set; } = "admin";
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }

        public ICollection<Messages> ReceivedMessages { get; set; } // Yöneticinin aldığı mesajlar

    }
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasMany(x => x.ReceivedMessages).WithOne(x => x.Admin)
                       .HasForeignKey(x => x.AdminId)
                        .IsRequired(true);

        }
    }
}
