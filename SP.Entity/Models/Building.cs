using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Entity.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string BuildingName { get; set; }
        public string BlockNumber { get; set; }
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
  
}

