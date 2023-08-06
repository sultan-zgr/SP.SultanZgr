using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SP.Entity.Models
{
    public class UserRole
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
    }
}