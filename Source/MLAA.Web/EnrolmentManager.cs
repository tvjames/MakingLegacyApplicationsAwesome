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
    public static class EnrolmentManager
    {
        /// <summary>
        ///     Is the Enrolled
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        public static bool IsEnrolled(int studentId, int subjectId)
        {
            try
            {
                //var sql = "SELECT COUNT(*) FROM StudentSubjectEnrolment WHERE StudentId = " + Authentication.CurrentUser.UserId
                //var sql = "SELECT COUNT(*) FROM StudentSubjectEnrolment WHERE SubjectId='"+subjectId+"'";
                var sql = "SELECT COUNT(*) FROM StudentSubjectEnrolment WHERE StudentId = " + Authentication.CurrentUser.UserId + " AND SubjectId='" + subjectId + "'";
                var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DerpUniversityConnectionString"].ConnectionString);
                connection.Open();
                var command = new SqlCommand(sql, connection);
                var result = (int) command.ExecuteScalar();
                if (result > 0) return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Searches for a student by name.
        /// </summary>
        /// <param name="name">Any Part of the first name or last name of the student.</param>
        /// <returns></returns>
        [Obsolete("Use the non-horrible version, please :)")]
        public static SqlDataReader SearchStudents(string name)
        {
            var sql = "SELECT * FROM Student WHERE LastName LIKE '" + name + "%'";
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DerpUniversityConnectionString"].ConnectionString);
            connection.Open();
            var command = new SqlCommand(sql, connection);
            var result = command.ExecuteReader();
            return result;
        }

        public static Student[] SearchStudentsNonHorrible(string name)
        {
            var context = new DerpUniversityDataContext(ConfigurationManager.ConnectionStrings["DerpUniversityConnectionString"].ConnectionString);
            return context.Students
                          .Where(s => s.LastName.StartsWith(name)) //FIXME bug? Should this be .Contains(name)?
                          .ToArray();
        }

        public static Subject[] GetStudentEnrolments(int name)
        {
            var context = new DerpUniversityDataContext(ConfigurationManager.ConnectionStrings["DerpUniversityConnectionString"].ConnectionString);
            var student = context.Students.First(s => s.Id == name);
            return student
                .StudentSubjectEnrolments
                .Select(sse => sse.Subject)
                .ToArray();
        }
    }
}