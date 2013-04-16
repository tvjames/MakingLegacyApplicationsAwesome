using System;
using MLAA.Data.Linq2Sql;

namespace MLAA.Web
{
    public class WebForm2ViewModel
    {
        public WebForm2ViewModel()
        {
            Students = new EnrolmentManager().SearchStudents("");
        }

        public Student[] Students { get; set; }

        [Obsolete("This is a dirty hack. Please refactor.")]
        public Subject[] GetStudentEnrolments(int studentId)
        {
            return new EnrolmentManager().GetStudentEnrolments(studentId);
        }
    }
}