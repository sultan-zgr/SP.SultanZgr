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
        public decimal Balance { get; set; }  //USERIN CÜZDANI
        public virtual List<PaymentResponse> Payments { get; set; }
        public int MonthlyInvoiceId { get; set; } // Kullanıcının aylık faturaları
    }
}
