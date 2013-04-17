using MLAA.Data.Linq2Sql;

namespace MLAA.Core.Domain.Entities
{
    public static class StudentExtensions
    {
        public static void EnrolIn(this Student student, Subject subject)
        {
            //FIXME precondition checks here

            subject.AcceptEnrolmentFor(student);
        }
    }
}