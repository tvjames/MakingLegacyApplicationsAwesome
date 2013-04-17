using System.Net.Mail;
using MLAA.Core;

namespace MLAA.Web.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        public void Send(MailMessage message)
        {
            var client = new SmtpClient("localhost");
            client.Send(message);
        }
    }
}