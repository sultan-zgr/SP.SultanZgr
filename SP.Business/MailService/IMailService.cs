using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Business.MailService
{
    public interface IMailService
    {
        Task SendReminderEmail(string userEmail);
    }
}
