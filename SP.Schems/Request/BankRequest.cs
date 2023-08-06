using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Request
{
    public class BankRequest
    {
        public string CreditCardNumber { get; set; } // Kredi kartı numarası
        public string ExpiryDate { get; set; } // Son kullanma tarihi (MM/YY formatında)
        public string CVV { get; set; } // Güvenlik kodu
    
  
}

}
