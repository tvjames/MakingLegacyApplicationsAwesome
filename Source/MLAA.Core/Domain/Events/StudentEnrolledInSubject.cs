using MLAA.Data.Linq2Sql;

namespace MLAA.Core.Domain.Events
{
    public class StudentEnrolledInSubject : IDomainEvent
    {
        private readonly Student _student;
        private readonly Subject _subject;

        public StudentEnrolledInSubject(Student student, Subject subject)
        {
            _student = student;
            _subject = subject;
        }

        public Student Student
        {
            get { return _student; }
        }

        public Subject Subject
        {
            get { return _subject; }
        }
    }
}