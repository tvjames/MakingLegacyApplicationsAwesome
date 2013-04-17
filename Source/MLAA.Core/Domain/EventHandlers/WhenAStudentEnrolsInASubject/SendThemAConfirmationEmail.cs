using System.Net.Mail;
using MLAA.Core.Domain.Events;

namespace MLAA.Core.Domain.EventHandlers.WhenAStudentEnrolsInASubject
{
    public class SendThemAConfirmationEmail : IHandle<StudentEnrolledInSubject>
    {
        private readonly IEmailSender _emailSender;

        public SendThemAConfirmationEmail(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void Handle(StudentEnrolledInSubject domainEvent)
        {
            const string @from = "dean@derpuniversity.example.com";
            var to = domainEvent.Student.EmailAddress;
            const string emailSubject = "Enrolment confirmation";
            var body = "Dear " + domainEvent.Student.FirstName + ",\n\n"
                       + "You have successfully enrolled in " + domainEvent.Subject.Code + ".\n\n"
                       + "Best of luck!\n\nSincerely,\nDean";

            var message = new MailMessage(from, to, emailSubject, body);
            _emailSender.Send(message);
        }
    }
}