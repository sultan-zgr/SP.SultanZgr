using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Request
{
    public class UserAccountRequest
    {
        public int UserId { get; set; }
        public decimal Balance { get; set; }
    }

}
