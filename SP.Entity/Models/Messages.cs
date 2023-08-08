using SP.Base.Enums.MessagesType;
using SP.Entity.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP.Entity
{
    public class Messages
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime Date { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public MessageStatus MessageStatus { get; set; }


    }

}

