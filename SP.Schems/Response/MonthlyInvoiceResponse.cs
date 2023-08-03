using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Response
{
    public class MonthlyInvoiceResponse
    {
        public int MonthlyInvoiceId { get; set; }
        public decimal DuesAmount { get; set; }
        public decimal WaterBill { get; set; }
        public decimal ElectricityBill { get; set; }
        public decimal GasBill { get; set; }
        public DateTime Date { get; set; }
        public ApartmentResponse Apartment { get; set; }

    }
}
