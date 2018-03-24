using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Tests.TestDatas.Courses;
using GradeTrackerApp.Interactors.Course;

namespace GradeTrackerApp.Tests.Mocks
{
    public class MockCourseInteractor : ICourseInteractor
    {
        public Guid CreateCourse(CourseEntity domainModel)
        {
            if (string.IsNullOrEmpty(domainModel.Name))
            {
                throw new MissingInfoException();
            }
            else
            {
                return Guid.NewGuid();
            }
        }
        
        public CourseEntity GetCourse(Guid courseId)
        {
            if (courseId.Equals(Guid.Empty))
                throw new ObjectNotFoundException();

            return CourseFactory.Create_CourseEntity_ValidMinimum(courseId);
        }

        public List<CourseEntity> GetAllCourses()
        {
            throw new NotImplementedException();
        }

        public List<CourseEntity> GetCoursesByStudentId(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
