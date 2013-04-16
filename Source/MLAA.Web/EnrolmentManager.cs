using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using MLAA.Data.Linq2Sql;

namespace MLAA.Web
{
    /// <summary>
    ///     This class is where everything about student enrokllments goes. DO NOT PUT ANYTHING ABOUT ENROLMENTS ANYWHERE ELSE
    ///     OR I WILL SHOUT AT YOU.
    /// </summary>
    public  class EnrolmentManager
    {
        private readonly DerpUniversityDataContext _context = new DerpUniversityDataContext(ConfigurationManager.ConnectionStrings["DerpUniversityConnectionString"].ConnectionString);

       
        public  bool IsEnrolled(int studentId, int subjectId)
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
        [Obsolete("Use the non-horrible version, please :)")]
        public  SqlDataReader SearchStudents(string name)
        {
            var sql = "SELECT * FROM Student WHERE LastName LIKE '" + name + "%'";
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DerpUniversityConnectionString"].ConnectionString);
            connection.Open();
            var command = new SqlCommand(sql, connection);
            var result = command.ExecuteReader();
            return result;
        }

        public  Student[] SearchStudentsNonHorrible(string name)
        {
            var context = new DerpUniversityDataContext(ConfigurationManager.ConnectionStrings["DerpUniversityConnectionString"].ConnectionString);
            return context.Students
                          .Where(s => s.LastName.StartsWith(name)) //FIXME bug? Should this be .Contains(name)?
                          .ToArray();
        }

        public  Subject[] GetStudentEnrolments(int name)
        {
            var student = _context.Students.First(s => s.Id == name);
            return student
                .StudentSubjectEnrolments
                .Select(sse => sse.Subject)
                .ToArray();
        }
    }
}