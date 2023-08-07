using SP.Base.Enums.Months;
using SP.Schema.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Request
{
    public class MonthlyInvoiceRequest
    {
        public int UserId { get; set; }
        public decimal InvoiceAmount { get; set; }
        public DateTime Date { get; set; }
        public Months MonthlyPayment { get; set; }


    }
}
