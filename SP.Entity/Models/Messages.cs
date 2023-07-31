using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Entity.Models;

namespace SP.Entity
{
    public class Messages
    {
        public int MessagesId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public int AdminId { get; set; }
        public virtual Admin Admin { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
    public class MessagesConfiguration : IEntityTypeConfiguration<Messages>
    {
        public void Configure(EntityTypeBuilder<Messages> builder)
        {
            builder.Property(x => x.Content).IsRequired();

            builder.Property(x => x.IsRead).IsRequired();

            builder.HasOne(x => x.Admin)                                     // Mesaj okuyan admin/yönetici
                   .WithMany(x => x.ReceivedMessages)
                   .HasForeignKey(x => x.AdminId)
                   .IsRequired(true);

            builder.HasOne(x => x.User)                                     // Mesaj göndern user
                   .WithMany(x => x.SentMessages)
                   .HasForeignKey(x => x.UserId)
                   .IsRequired(true);


            // Diğer ilişkiler ve yapılandırmalar burada olabilir
        }
    }

}
