using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;

namespace GradeTrackerApp.Interactors.Semester
{
    public interface ISemesterInteractor
    {
        SemesterEntity GetSemester(Guid semesterId);
        List<SemesterEntity> GetAllSemesters();
    }
}