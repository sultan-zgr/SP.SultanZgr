using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Request.UserLog
{
    public class UserLogRequest
    {
        public string UserName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string LogType { get; set; }
    }
}
