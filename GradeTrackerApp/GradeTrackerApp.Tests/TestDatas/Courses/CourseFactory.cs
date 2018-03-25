using GradeTrackerApp.Domain.Courses.Models;
using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.Tests.TestDatas.Courses
{
    public static class CourseFactory
    {
        public static CreateCourseDomainModel Create_CreateCourseDomainModel_ValidMinimum()
        {
            return new CreateCourseDomainModel
            {
                StudentId = Guid.NewGuid(),
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
                StudentId = Guid.NewGuid(),
                Name = "Intro to Physics",
                Department = "PHYS",
                Number = "1145",
                SemesterId = Guid.NewGuid(),
                Year = 2018,
                IsActive = true
            };
        }

        public static CourseEntity Create_CourseEntity_ValidMinimum(Guid courseId)
        {
            return new CourseEntity
            {
                Id = courseId,
                StudentId = Guid.NewGuid(),
                Name = "Intro to Physics",
                Department = "PHYS",
                Number = "1145",
                SemesterId = Guid.NewGuid(),
                Year = 2018,
                IsActive = true
            };
        }

        public static CourseEntity Create_CourseEntity_ValidMinimum_CustomStudentId(Guid studentId)
        {
            return new CourseEntity
            {
                StudentId = studentId,
                Name = "Intro to Physics",
                Department = "PHYS",
                Number = "1145",
                SemesterId = Guid.NewGuid(),
                Year = 2018,
                IsActive = true
            };
        }

        public static List<CourseEntity> Create_TwoCourseEntities_ValidMinimum_CustomStudentId(Guid studentId)
        {
            var returnList = new List<CourseEntity>();

            returnList.Add(Create_CourseEntity_ValidMinimum_CustomStudentId(studentId));
            returnList.Add(Create_CourseEntity_ValidMinimum_CustomStudentId(studentId));

            return returnList;
        }

        public static CreateCourseDomainModel CreateCCreateCourseDomainModel_Empty()
        {
            return new CreateCourseDomainModel();
        }
    }
}
