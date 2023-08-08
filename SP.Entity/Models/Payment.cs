using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Entity;
using SP.Entity.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patika.Entity.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; } // Ödeme tarihi
        public decimal InvoiceAmount { get; set; } // Ödeme miktarı
        public int UserId { get; set; } // Ödeme yapan kullanıcının ID'si
        public int MonthlyInvoiceId { get; set; }
        public decimal Balance { get; set; }  //USERIN CÜZDANI
        public virtual MonthlyInvoice MonthlyInvoice { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public decimal NewBalance { get; set; } // Ödeme sonrasında güncellenen cüzdan bakiyesi 

      

        }
    }

