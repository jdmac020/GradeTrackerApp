using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Tests.TestDatas.Courses;
using GradeTrackerApp.Interactors.Course;
using GradeTrackerApp.Interactors.Evaluation;
using GradeTrackerApp.Tests.TestDatas.Evaluations;

namespace GradeTrackerApp.Tests.Mocks
{
    public class MockEvaluationInteractor : IEvaluationInteractor
    {

        public Guid CreateEvaluation(EvaluationEntity newEvaluationEntity)
        {
            if (string.IsNullOrEmpty(newEvaluationEntity.Name))
                throw new MissingInfoException();
            if (newEvaluationEntity.CourseId.Equals(Guid.Empty))
                throw new MissingInfoException();
            if (newEvaluationEntity.WeightId.Equals(Guid.Empty))
                throw new MissingInfoException();
            if (newEvaluationEntity.NumberOfScores < 0 )
                throw new MissingInfoException();

            return Guid.NewGuid();
        }

        public EvaluationEntity GetEvaluation(Guid evaluationId)
        {
            if (evaluationId.Equals(Guid.Empty))
                throw new ObjectNotFoundException();

            return EvaluationFactory.Create_EvaluationEntity_ValidMinimum(evaluationId);
        }
    }
}
