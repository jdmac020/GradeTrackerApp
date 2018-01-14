using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Tests.Mocks;

namespace GradeTrackerApp.Tests.TestValues.Courses
{
    public static class ServiceFactory
    {
        public static CourseService CreateCourse_MockInteractor()
        {
            var interactor = new MockCreateCourse();

            return new CourseService(interactor);
        }
    }
}
