namespace MLAA.Web
{
    public class WebForm1ViewModel
    {
        private readonly EnrolmentManager _enrolmentManager;

        public WebForm1ViewModel(EnrolmentManager enrolmentManager)
        {
            _enrolmentManager = enrolmentManager;
        }

        public bool IsEnrolled(int userId, int subjectId)
        {
            return _enrolmentManager.IsEnrolled(userId, subjectId);
        }
    }
}