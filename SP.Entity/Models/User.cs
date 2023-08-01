using Patika.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Entity.Models
{
    public class User //NORMAL KULLANICI
    {
        public int UserId { get; set; }
        public string TCNo { get; set; }
        public string VehiclePlateNumber { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; } // Kullanıcının sahip olduğu daireler
        public virtual ICollection<Payment> Payments { get; set; } // Kullanıcının yapmış olduğu ödemeler

    }
}
