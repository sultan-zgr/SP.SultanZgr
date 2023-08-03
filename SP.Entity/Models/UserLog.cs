using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Entity.Models
{
    public class UserLog
    {
        public int UserLogId { get; set; }
        public string UserName { get; set; }
        public DateTime PaymentDate { get; set; }
        public string LogType { get; set; }
        
    }
    public class UserLogConfiguration : IEntityTypeConfiguration<UserLog>
    {
        public void Configure(EntityTypeBuilder<UserLog> builder)
        {
            // Set the table name for the entity
            builder.ToTable("UserLogs");

            // Set the primary key for the entity
            builder.HasKey(x => x.UserLogId);

            // Set column configurations
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.PaymentDate).IsRequired();
            builder.Property(x => x.LogType).IsRequired().HasMaxLength(50);
        }
    }
}