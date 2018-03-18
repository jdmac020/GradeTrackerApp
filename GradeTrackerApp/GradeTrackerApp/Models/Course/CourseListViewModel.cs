using System.Collections.Generic;

namespace GradeTrackerApp.Models.Course
{
    public class CourseListViewModel : List<CourseViewModel>
    {
        public CourseListViewModel(IEnumerable<CourseViewModel> courses)
        {
            foreach (var course in courses)
            {
                Add(course);
            }
            
        }
    }
}