using System;
using System.Linq;
using MLAA.Core.Domain.Entities;
using MLAA.Data.Linq2Sql;

namespace MLAA.Web
{
    public class EnrolmentManager
    {
        private readonly DerpUniversityDataContext _context;

        public EnrolmentManager(DerpUniversityDataContext context)
        {
            _context = context;
        }

        public bool IsEnrolled(int studentId, int subjectId)
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
        public Student[] SearchStudents(string name)
        {
            return _context.Students
                           .Where(s => s.LastName.StartsWith(name)) //FIXME bug? Should this be .Contains(name)? According to the XMLDoc it's a bug...
                           .ToArray();
        }

        public Subject[] GetStudentEnrolments(int name)
        {
            var student = _context.Students.First(s => s.Id == name);
            return student
                .StudentSubjectEnrolments
                .Select(sse => sse.Subject)
                .ToArray();
        }

        public void ToggleStudentEnrolment(int studentId, int subjectId)
        {
            var student = GetStudentById(studentId);
            var subject = GetSubjectById(subjectId);

            if (IsEnrolled(studentId, subjectId))
            {
                throw new NotImplementedException();
            }
            else
            {
                student.EnrolIn(subject);
            }

            _context.SubmitChanges(); //FIXME this is a horrible hack.
        }

        private Student GetStudentById(int studentId)
        {
            return _context.Students
                           .Where(s => s.Id == studentId)
                           .Single();
        }

        private Subject GetSubjectById(int subjectId)
        {
            return _context.Subjects
                           .Where(s => s.Id == subjectId)
                           .Single();
        }

        public StudentEnrolmentDto[] GetAllStudentEnrolments()
        {
            var result = _context.Students.Select(student => new StudentEnrolmentDto
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Subjects = student.StudentSubjectEnrolments
                                  .Select(sse => new SubjectDto
                                  {
                                      Code = sse.Subject.Code,
                                      Name = sse.Subject.Name
                                  }).ToArray()
            });
            return result.ToArray();
        }
    }
}