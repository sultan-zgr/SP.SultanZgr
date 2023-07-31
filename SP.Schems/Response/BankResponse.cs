using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Response
{
    public class BankResponse
    {
        public bool IsSuccessful { get; set; } // İşlem başarılı mı?
        public string Message { get; set; } // İşlemle ilgili mesaj
    }
}