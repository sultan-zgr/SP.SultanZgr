using SP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Response
{
    public class AdminResponse
    {
        //public string Owner_Tenant { get; set; } // Sahibi mi Kiracı mı
        //public string Type { get; set; }  // 2+1 3+1 vs..
        //public bool IsOccupied { get; set; }
        //public string BlockName { get; set; }
        //public int FloorNumber { get; set; }
        //public int ApartmentNumber { get; set; }
        public virtual ICollection<MonthlyInvoice> MonthlyInvoices { get; set; }
    }
}
