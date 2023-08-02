using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Entity.Models
{
    public class Bank
    {
        public int BankId { get; set; }
        public string CreditCardNumber { get; set; } // Kullanıcının kredi kartı numarası
        public string ExpiryDate { get; set; } // Kredi kartının son kullanma tarihi (MM/YY formatında)
        public string CVV { get; set; } // Kredi kartının güvenlik kodu
        public decimal Amount { get; set; } // Ödeme miktarı
    }

}
