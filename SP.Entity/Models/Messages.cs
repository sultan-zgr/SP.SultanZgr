using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Entity.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP.Entity
{
    public class Messages
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public int ApartmentId { get; set; } // Change this to ApartmentId
        [ForeignKey("SenderId")]
        public User Sender { get; set; }
    }


}

