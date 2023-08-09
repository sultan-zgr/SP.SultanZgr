using SP.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Request
{
    public class CashRequest
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public int MonthlyInvoiceId { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }

    }

}
