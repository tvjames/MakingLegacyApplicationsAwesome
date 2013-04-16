using System.Configuration;
using System.Data.SqlClient;

namespace MLAA.Web
{
    /// <summary>
    /// This class is where everything about student enrokllments goes. DO NOT PUT ANYTHING ABOUT ENROLMENTS ANYWHERE ELSE
    /// OR I WILL SHOUT AT YOU.
    /// </summary>
    public static class EnrolmentManager
    {
        /// <summary>
        /// Is the Enrolled
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
        /// Searches for a student by name.
        /// </summary>
        /// <param name="name">Any Part of the first name or last name of the student.</param>
        /// <returns></returns>
        public static SqlDataReader SearchStudents(string name)
        {
            var sql = "SELECT * FROM Student WHERE LastName LIKE '"+name+"%'";
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DerpUniversityConnectionString"].ConnectionString);
            connection.Open();
            var command = new SqlCommand(sql, connection);var result = command.ExecuteReader();
            return result;
        }

        /// <summary>
        /// Searches for a student by name.
        /// </summary>
        /// <param name="name">Any Part of the first name or last name of the student.</param>
        /// <returns></returns>
        public static SqlDataReader GetSTUdentEnrolments(int name)
        {
            //var sql = "SELECT * FROM Student WHERE LastName LIKE '%" + name + "%'";
            //var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DerpUniversityConnectionString"].ConnectionString);
            //connection.Open();
            //var command = new SqlCommand(sql, connection);
            //var result = command.ExecuteReader();
            //return result; 
            var sql = "SELECT * FROM Subject AS sse INNER JOIN StudentSubjectEnrolment AS s ON sse.Id = s.StudentId WHERE s.StudentId=" + name;
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DerpUniversityConnectionString"].ConnectionString);
            connection.Open();
            var command = new SqlCommand(sql, connection);
            var result = command.ExecuteReader();
            return result;
        }
    }
}