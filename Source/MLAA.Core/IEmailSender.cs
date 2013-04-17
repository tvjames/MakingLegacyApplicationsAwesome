using System.Net.Mail;

namespace MLAA.Core
{
    public interface IEmailSender
    {
        void Send(MailMessage message);
    }
}