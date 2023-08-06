using SP.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Response
{
    public class PaymentResponse
    {
        public int PaymentId { get; set; } // Ödeme işlemine atanan kimlik numarası
       
        public string Message { get; set; } // Ödeme işlemiyle ilgili bir mesaj (isteğe bağlı, hata durumunda kullanılabilir)
        public decimal NewBalance { get; set; } // Ödeme sonrasında güncellenen cüzdan bakiyesi (isteğe bağlı)
        public DateTime PaymentDate { get; set; }
      
    }
}
