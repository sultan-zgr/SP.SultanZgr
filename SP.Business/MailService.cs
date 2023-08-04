using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;
    using System.Threading.Tasks;
namespace SP.Business
{

    public interface IMailService
    {
        Task SendReminderEmail(string userEmail);
    }
    public class MailService : IMailService
    {
        private readonly SmtpSettings _smtpSettings;

        public MailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendReminderEmail(string userEmail)
        {
            try
            {
                var fromEmail = _smtpSettings.AdminEmail; // Admin email'i kullanmak istediğinizden emin olun
                var subject = "Ödeme Hatırlatması";
                var body = "Ödemenizi yapmayı unutmayınız!";

                var mailMessage = new MailMessage(fromEmail, userEmail, subject, body);
                var smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
                {
                    Credentials = new System.Net.NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                    EnableSsl = true
                };

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while sending email: " + ex.Message);
            }
        }
    }

}
