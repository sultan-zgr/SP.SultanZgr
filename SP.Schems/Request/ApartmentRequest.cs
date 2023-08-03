using SP.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Request
{
    public class ApartmentRequest
    {
        public int UserId { get; set; }  
        public int BuildingId { get; set; }  
        public bool IsOccupied { get; set; }
        public bool IsOwner { get; set; }
        public string Type { get; set; }
        public string BlockName { get; set; }
        public int FloorNumber { get; set; }
        public int ApartmentNumber { get; set; }

       
    }


}
