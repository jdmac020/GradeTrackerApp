using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.Interactors.Course
{
    public interface ICourseInteractor
    {
        CourseEntity CreateCourse(CourseEntity domainModel);
    }
}