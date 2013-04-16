using System;
using System.Web.UI;
using MLAA.Data.Linq2Sql;

namespace MLAA.Web
{
    public partial class WebForm2 : Page
    {
        public WebForm2ViewModel ViewModel { get; set; }
    }

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