using SP.Entity.Models;
using SP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Schema.Response;

namespace SP.Schema.Request
{
    public class PaymentRequest
    {
        public int UserId { get; set; } 
        public decimal Wallet { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public int MonthlyInvoiceId { get; set; }


    }
}
