using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Patika.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Entity.Models;
using SP.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using SP.Base.Enums.Months;

namespace SP.Entity
{
    public class MonthlyInvoice
    {
        public int MonthlyInvoiceId { get; set; }
        public decimal InvoiceAmount { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }  
        public bool InvoiceStatus { get; set; }
        public virtual User User { get; set; }  
        public virtual ICollection<Payment> Payments { get; set; }
        public Months Months { get; set; }  
    }



}

