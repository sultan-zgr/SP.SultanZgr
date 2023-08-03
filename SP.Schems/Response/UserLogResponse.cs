using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Response
{
    public class UserLogResponse
    {
        public string UserName { get; set; }
        public DateTime PaymentDate { get; set; }
        public string LogType { get; set; }
    }
}
