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
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int MonthlyInvoiceId { get; set; } // Include this property if needed
    }

}
