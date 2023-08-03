using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Response
{
    public class PaymentResponse
    {
        public int Id { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } 
        public DateTime PaymentDate { get; set; }
    }
}
