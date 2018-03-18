using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Tests.TestDatas.Semesters;
using GradeTrackerApp.Interactors.Course;
using GradeTrackerApp.Interactors.Semester;

namespace GradeTrackerApp.Tests.Mocks
{
    public class MockSemesterInteractor : ISemesterInteractor
    {
        
        public SemesterEntity GetSemester(Guid semesterId)
        {
            if (semesterId.Equals(Guid.Empty))
                throw new ObjectNotFoundException();

            return SemesterFactory.Create_SemesterEntity_ValidMinimums(semesterId);
        }

        public List<SemesterEntity> GetAllSemesters()
        {
            return SemesterFactory.Create_SemesterEntity_ListOfAll();
        }
    }
}
