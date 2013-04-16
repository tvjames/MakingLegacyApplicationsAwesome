using System;
using MLAA.Data.Linq2Sql;

namespace MLAA.Web
{
    public class WebForm2ViewModel
    {
        private readonly EnrolmentManager _enrolmentManager;

        public WebForm2ViewModel(EnrolmentManager enrolmentManager)
        {
            _enrolmentManager = enrolmentManager;

            Students = _enrolmentManager.SearchStudents("");
        }

        public Student[] Students { get; set; }

        [Obsolete("This is a dirty hack. Please refactor.")]
        public Subject[] GetStudentEnrolments(int studentId)
        {
            return _enrolmentManager.GetStudentEnrolments(studentId);
        }
    }
}