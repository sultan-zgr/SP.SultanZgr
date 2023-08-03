using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SP.Entity.Models;
using Patika.Entity.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP.Entity
{
    public class Apartment
    {
        public int Id { get; set; }
        public bool IsOccupied { get; set; }
        public bool IsOwner { get; set; } = true;
        public string Type { get; set; }
        public int FloorNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public int UserId { get; set; }
        public int BuildingId { get; set; }
        public virtual ICollection<MonthlyInvoice> MonthlyInvoices { get; set; }
        public virtual User User { get; set; }
    }



  
}

