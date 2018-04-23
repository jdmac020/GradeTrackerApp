﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Courses.Service;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Tests.TestDatas.Courses;

namespace GradeTrackerApp.Tests.Mocks
{
    public class MockCourseService_Positives : ICourseService
    {
        public IDomainModel CreateCourse(CreateCourseDomainModel createModel)
        {
            throw new NotImplementedException();
        }

        public IDomainModel GetCourse(Guid courseId)
        {
            return new CourseDomainModel();
        }

        public List<IDomainModel> GetCourses(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}