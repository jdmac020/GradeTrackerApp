using System;
using System.Collections.Generic;
using GradeTrackerApp.Domain.Semesters.Models;

namespace GradeTrackerApp.Domain.Semesters.Service
{
    public interface ISemesterService
    {
        IDomainModel GetSemester(Guid semesterId);
        List<IDomainModel> GetAllSemesters();
    }
}