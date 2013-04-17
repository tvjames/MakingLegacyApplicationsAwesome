namespace MLAA.Web
{
    public class StudentEnrolmentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SubjectDto[] Subjects { get; set; }
    }
}