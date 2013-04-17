using System;
using System.Linq;
using MLAA.Core;
using MLAA.Core.Domain.Entities;
using MLAA.Core.Domain.Events;
using MLAA.Data.Linq2Sql;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace MLAA.UnitTests.StudentTests
{
    [TestFixture]
    public class WhenAStudentEnrolsInASubject
    {
        private IEventBroker _fakeEventBroker;
        private Student _fred;
        private Subject _subject;

        [SetUp]
        public void SetUp()
        {
            _fakeEventBroker = Substitute.For<IEventBroker>();
            DomainEvents.SetEventBrokerStrategy(_fakeEventBroker);

            _fred = new Student();
            _subject = new Subject();

            _fred.EnrolIn(_subject);
        }

        [Test]
        public void TheyShouldHaveACorrespondingEnrolment()
        {
            var enrolments = _subject.StudentSubjectEnrolments
                                     .Where(sse => sse.Student == _fred)
                                     .Where(sse => sse.Subject == _subject)
                                     .ToArray();
            enrolments.Count().ShouldBe(1);
        }

        [Test]
        public void WeShouldSendThemAConfirmationEmail()
        {
            _fakeEventBroker
                .Received()
                .Raise(Arg.Any<StudentEnrolledInSubject>());
        }
    }

    [TestFixture]
    public class WhenAStudentTriesToEnrolInASubjectThatHasNoRoom
    {
        private IEventBroker _fakeEventBroker;
        private Student _fred;
        private Subject _law;

        [SetUp]
        public void SetUp()
        {
            _fakeEventBroker = Substitute.For<IEventBroker>();
            DomainEvents.SetEventBrokerStrategy(_fakeEventBroker);

            _fred = new Student();
            _law = new Subject
            {
                MaxStudents = 0,
            };
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void TheyShouldNotBePermitted()
        {
            _fred.EnrolIn(_law);
        }

        [Test]
        public void ThereShouldBeNoEventRaised()
        {
            try
            {
                _fred.EnrolIn(_law);
            }
            catch (InvalidOperationException e)
            {
            }

            _fakeEventBroker
                .DidNotReceive()
                .Raise(Arg.Any<StudentEnrolledInSubject>());
        }
    }
}