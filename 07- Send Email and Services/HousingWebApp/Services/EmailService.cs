using System.Net;
using System.Net.Mail;

namespace HousingWebApp.Services
{
    public class EmailService
    {
        private IConfiguration Config;
        public EmailService(IConfiguration config)
        {
            Config = config;
        }

        public async Task SendEmailAsync(string toemail, string subject, 
            string message, bool isHtml=true)
        {
            var section = Config.GetSection("Email");
            //TBD - check if section is null...
            if(section != null)
            {
                string from_email = section["from:mail"];
                string from_displayName = section["from:display"];
                string password = section["password"];

                var fromEmail = new MailAddress(from_email, from_displayName);
                var toEmail = new MailAddress(toemail);
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Timeout = 5000,
                    Credentials = new NetworkCredential(fromEmail.Address, password)
                };
                using var messageObj = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = isHtml
                };
                try
                {
                    await smtp.SendMailAsync(messageObj);
                }
                catch
                {

                }
            }
        }
    }
}
