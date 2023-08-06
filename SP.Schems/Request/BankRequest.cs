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
    
    public List<string> Validate()
    {
        List<string> validationErrors = new List<string>();

        if (string.IsNullOrEmpty(CreditCardNumber) || CreditCardNumber.Length != 19)
        {
            validationErrors.Add("Kredi kartı numarası geçersiz. 19 karakter olmalıdır: \"1234 5678 9012 3456\"");
        }

        if (string.IsNullOrEmpty(ExpiryDate) || ExpiryDate.Length != 5)
        {
            validationErrors.Add("Son kullanma tarihi geçersiz. 5 karakter olmalıdır: \"MM/YY\"");
        }

        if (string.IsNullOrEmpty(CVV) || CVV.Length != 3)
        {
            validationErrors.Add("CVV geçersiz. 3 karakter olmalıdır");
        }

        return validationErrors;
    }
}

}
