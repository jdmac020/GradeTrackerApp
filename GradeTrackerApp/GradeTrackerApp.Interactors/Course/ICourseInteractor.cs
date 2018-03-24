﻿using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.Interactors.Course
{
    public interface ICourseInteractor
    {
        Guid CreateCourse(CourseEntity domainModel);

        CourseEntity GetCourse(Guid courseId);
        List<CourseEntity> GetAllCourses();
    }
}