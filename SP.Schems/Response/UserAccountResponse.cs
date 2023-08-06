using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Response
{
    public class UserAccountResponse
    {
        public int UserId { get; set; }
        public decimal Balance { get; set; } 
        public virtual List<PaymentResponse> Payments { get; set; }
        public int MonthlyInvoiceId { get; set; } 
    }
}
