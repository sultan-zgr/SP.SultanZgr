using SP.Base.Enums.Months;
using SP.Entity.Models;
using SP.Schema.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Response
{
    public class MonthlyInvoiceResponse
    {
        public int UserId { get; set; }
        public decimal InvoiceAmount { get; set; }
        public DateTime Date { get; set; }
        public bool InvoiceStatus { get; set; }
        public Months Months { get; set; }

    }
}
