using Microsoft.AspNetCore.Identity;
using Patika.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Entity.Models
{
    public class AppUser : IdentityUser  //LOGİN / ROL ATAMA
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
