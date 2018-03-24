using GradeTrackerApp.Domain.Courses.Models;
using System;
using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.Tests.TestDatas.Courses
{
    public static class CourseFactory
    {
        public static CreateCourseDomainModel Create_CreateCourseDomainModel_ValidMinimum()
        {
            return new CreateCourseDomainModel
            {
                Name = "Intro to Physics",
                Department = "PHYS",
                Number = "1145",
                SemesterId = Guid.NewGuid(),
                Year = 2018
            };
        }

        public static CourseEntity Create_CourseEntity_ValidMinimum()
        {
            return new CourseEntity
            {
                
                Name = "Intro to Physics",
                Department = "PHYS",
                Number = "1145",
                SemesterId = Guid.NewGuid(),
                Year = 2018
            };
        }

        public static CourseEntity Create_CourseEntity_ValidMinimum(Guid courseId)
        {
            return new CourseEntity
            {
                Id = courseId,
                Name = "Intro to Physics",
                Department = "PHYS",
                Number = "1145",
                SemesterId = Guid.NewGuid(),
                Year = 2018
            };
        }

        public static CreateCourseDomainModel CreateCCreateCourseDomainModel_Empty()
        {
            return new CreateCourseDomainModel();
        }
    }
}
