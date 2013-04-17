using System;

namespace MLAA.Web
{
    public class WebForm2ViewModel
    {
        private readonly EnrolmentManager _enrolmentManager;
        private readonly Lazy<StudentEnrolmentDto[]> _studentEnrolments;

        public WebForm2ViewModel(EnrolmentManager enrolmentManager)
        {
            _enrolmentManager = enrolmentManager;

            _studentEnrolments = new Lazy<StudentEnrolmentDto[]>(
                _enrolmentManager.GetAllStudentEnrolments, false);
        }

        public StudentEnrolmentDto[] StudentEnrolments
        {
            get { return _studentEnrolments.Value; }
        }
    }
}