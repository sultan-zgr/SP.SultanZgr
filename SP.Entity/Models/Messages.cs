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
       

    }
    public class MessagesConfiguration : IEntityTypeConfiguration<Messages>
    {
        public void Configure(EntityTypeBuilder<Messages> builder)
        {
            builder.Property(x => x.Content).IsRequired();

            builder.Property(x => x.IsRead).IsRequired();

           


            // Diğer ilişkiler ve yapılandırmalar burada olabilir
        }
    }

}
