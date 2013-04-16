using System.Configuration;
using System.Linq;
using MLAA.Data.Linq2Sql;

namespace MLAA.Web
{
    public class EnrolmentManager
    {
        private readonly DerpUniversityDataContext _context =
            new DerpUniversityDataContext(ConfigurationManager.ConnectionStrings["DerpUniversityConnectionString"].ConnectionString);

        public bool IsEnrolled(int studentId, int subjectId)
        {
            return _context
                .StudentSubjectEnrolments
                .Where(sse => sse.StudentId == studentId)
                .Where(sse => sse.SubjectId == subjectId)
                .Any();
        }

        /// <summary>
        ///     Searches for a student by name.
        /// </summary>
        /// <param name="name">Any Part of the first name or last name of the student.</param>
        /// <returns></returns>
        public Student[] SearchStudents(string name)
        {
            return _context.Students
                           .Where(s => s.LastName.StartsWith(name)) //FIXME bug? Should this be .Contains(name)? According to the XMLDoc it's a bug...
                           .ToArray();
        }

        public Subject[] GetStudentEnrolments(int name)
        {
            var student = _context.Students.First(s => s.Id == name);
            return student
                .StudentSubjectEnrolments
                .Select(sse => sse.Subject)
                .ToArray();
        }
    }
}