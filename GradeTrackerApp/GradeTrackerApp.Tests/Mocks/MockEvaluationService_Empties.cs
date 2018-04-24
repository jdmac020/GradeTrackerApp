using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Domain.Evaluations.Service;
using GradeTrackerApp.Domain.Shared;

namespace GradeTrackerApp.Tests.Mocks
{
    public class MockEvaluationService_Empties : IEvaluationService
    {
        public IDomainModel CreateNewEvaluation(CreateEvaluationDomainModel createModel)
        {
            throw new NotImplementedException();
        }

        public IDomainModel GetEvaluation(Guid evaluationId)
        {
            return new EvaluationDomainModel();
        }

        public List<IDomainModel> GetEvaluationsForCourse(Guid courseId)
        {
            return new List<IDomainModel>();
        }
    }
}
