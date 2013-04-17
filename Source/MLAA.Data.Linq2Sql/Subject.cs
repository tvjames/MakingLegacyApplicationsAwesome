namespace MLAA.Data.Linq2Sql
{
    public partial class Subject
    {
        partial void OnCreated()
        {
            MaxStudents = 100;
        }
    }
}