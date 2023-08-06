using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace SP.Entity.Models
{
    public class Bank
    {
        public int BankId { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CVV { get; set; }
       
    }

   
    }

