using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Patika.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Entity.Models
{

    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string TCNo { get; set; }
        public string VehiclePlateNumber { get; set; }

        // Diğer özel alanlarınızı da ekleyebilirsiniz.

        // İlişkiler
        public List<Messages> Messages { get; set; } // Kullanıcının gönderdiği mesajlar
        public List<Payment> Payments { get; set; } // Kullanıcının yaptığı ödemeler
        public List<Apartment> Apartments { get; set; } // Kullanıcının sahip olduğu daireler
    }

    }


