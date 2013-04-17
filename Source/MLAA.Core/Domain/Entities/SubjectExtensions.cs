using System;
using MLAA.Core.Domain.Events;
using MLAA.Data.Linq2Sql;

namespace MLAA.Core.Domain.Entities
{
    public static class SubjectExtensions
    {
        public static void AcceptEnrolmentFor(this Subject subject, Student student)
        {
            if (subject.StudentSubjectEnrolments.Count >= subject.MaxStudents)
                throw new InvalidOperationException(
                    "Too many students. No room for you");

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