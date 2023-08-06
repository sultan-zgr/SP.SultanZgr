using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Timers;

namespace SP.Business.MailService
{
  public interface IMailService
    {
        Task SendReminderEmail(string userEmail);
    }
    public class MailService : IMailService
    {
        private readonly System.Timers.Timer _timer;
        private readonly SmtpSettings _smtpSettings;

        public MailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
            _timer = new System.Timers.Timer(86400000); // 24 hours in milliseconds
            _timer.Elapsed += async (sender, e) => await SendReminderEmail(_smtpSettings.AdminEmail);
            _timer.Start();
        }

        public async Task SendReminderEmail(string userEmail)
        {
            try
            {
                var fromEmail = _smtpSettings.AdminEmail;
                var subject = "Payment Reminder";
                var body = "Don't forget to make your payment!";

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