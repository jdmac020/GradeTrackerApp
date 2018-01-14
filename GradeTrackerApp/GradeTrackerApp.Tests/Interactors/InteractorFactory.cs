using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.EntityFramework.Entities;
using GradeTrackerApp.Interactors.Course;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.Interactors
{
    public static class InteractorFactory
    {
        public static CreateCourse CreateCourse_MockRepo()
        {
            var mockRepo = new MockRepository<CourseEntity, Guid>();

            return new CreateCourse(mockRepo);
        }
    }
}
