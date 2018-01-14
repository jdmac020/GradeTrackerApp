using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Domain.Models.Courses;

namespace GradeTrackerApp.Tests.TestValues.Courses
{
    public static class CourseFactory
    {
        public static CreateCourseDomainModel Create_CreateCourseDomainModel_ValidMinimum()
        {
            return new CreateCourseDomainModel
            {
                Name = "Intro to Physics",
                Department = "PHYS",
                StartDate = DateTime.Parse("1/15/2018"),
                EndDate = DateTime.Parse("5/28/2018"),
                EvaluationCount = 0,
                Number = "1145",
                SchoolId = Guid.Empty,
                SemesterId = Guid.Empty,
                Year = 2018
            };
        }
    }
}
