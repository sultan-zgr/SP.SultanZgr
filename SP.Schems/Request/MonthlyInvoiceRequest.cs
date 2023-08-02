using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Request
{
    public class MonthlyInvoiceRequest
    {
        public decimal DuesAmount { get; set; }
        public decimal WaterBill { get; set; }
        public decimal ElectricityBill { get; set; }
        public decimal GasBill { get; set; }
        public DateTime Date { get; set; }
        public ApartmentRequest Apartment { get; set; } 
    }
}
