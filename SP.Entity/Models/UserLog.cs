using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Entity
{
    public class UserLog
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string LogType { get; set; }
    }


    public class UserLogConfiguration : IEntityTypeConfiguration<UserLog>
    {
        public void Configure(EntityTypeBuilder<UserLog> builder)
        {
            builder.Property(x => x.Id).IsRequired(true).UseIdentityColumn();

            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.TransactionDate).IsRequired(true);
            builder.Property(x => x.LogType).IsRequired(true).HasMaxLength(20);
        }
    }
}

