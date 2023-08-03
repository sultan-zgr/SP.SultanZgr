using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Response
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public decimal Wallet { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

    }
}
