using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Request
{
    public class PaymentRequest
    {
        public int UserId { get; set; }
        public string CreditCardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CVV { get; set; }
        public decimal Amount { get; set; }
    }
}
