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
        public int UserId { get; set; } // Ödeme yapan kullanıcının ID'si
        public decimal Wallet { get; set; }
        public DateTime PaymentDate { get; set; } // Ödeme tarihi
        public decimal InvoiceAmount { get; set; } // Ödeme miktarı
        public int MonthlyInvoiceId { get; set; }
       // public Bank Bank { get; set; }

    }
}
