using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Semesters.Service;
using GradeTrackerApp.Domain.Shared;

namespace GradeTrackerApp.Tests.Mocks
{
    public class MockSemesterService_Fails : ISemesterService
    {
        public IDomainModel GetSemester(Guid semesterId)
        {
            var exception = new ObjectNotFoundException("Semester Wasn't Found");

            return new ErrorDomainModel(exception, false);
        }

        public List<IDomainModel> GetAllSemesters()
        {
            var exception = new ObjectNotFoundException("Semesters Weren't Found");

            return new List<IDomainModel> { new ErrorDomainModel(exception, false)};
        }
    }
}
