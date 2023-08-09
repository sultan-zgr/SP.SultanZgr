using SP.Base.Enums.MessagesType;
using SP.Entity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Schema.Response
{
    public class MessagesResponse
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public MessageStatus MessageStatus { get; set; }
    }
}
