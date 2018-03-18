using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Interactors.Semester;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.TestDatas.Semesters
{
    public static class InteractorFactory
    {

        public static SemesterInteractor Create_SemesterInteractor()
        {
            var mockRepo = new MockRepository<SemesterEntity>();

            return new SemesterInteractor(mockRepo);
        }

        public static SemesterInteractor Create_SemesterInteractor(MockRepository<SemesterEntity> mockRepo)
        {
            return new SemesterInteractor(mockRepo);
        }

        public static ISemesterInteractor Create_MockSemesterInteractor()
        {
            return new MockSemesterInteractor();
        }
    }
}
