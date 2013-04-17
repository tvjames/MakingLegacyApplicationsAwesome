using MLAA.Core.Domain.Events;
using MLAA.Data.Linq2Sql;

namespace MLAA.Core.Domain.Entities
{
    public static class SubjectExtensions
    {
        public static void AcceptEnrolmentFor(this Subject subject, Student student)
        {
            //FIXME precondition checks here

            var enrolment = new StudentSubjectEnrolment
            {
                Student = student,
                Subject = subject,
            };

            subject.StudentSubjectEnrolments.Add(enrolment);

            DomainEvents.Raise(new StudentEnrolledInSubject(student, subject));
        }
    }
}