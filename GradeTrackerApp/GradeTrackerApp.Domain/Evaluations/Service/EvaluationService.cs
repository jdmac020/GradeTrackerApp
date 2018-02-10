using System;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Interactors.Course;
using GradeTrackerApp.Interactors.Evaluation;

namespace GradeTrackerApp.Domain.Evaluations.Service
{
    public class EvaluationService : IEvaluationService
    {
        #region Services



        #endregion

        #region Interactors


        private IEvaluationInteractor EvaluationInteractor
        {
            get { return _evaluationInteractor ?? (_evaluationInteractor = new EvaluationInteractor()); }
            set { _evaluationInteractor = value; }
        }

        private IEvaluationInteractor _evaluationInteractor;

        #endregion

        /// <summary>
        /// Constructor to override default interactor
        /// </summary>
        /// <param name="evaluationInteractor">Interactor to be used by class</param>
        public EvaluationService(IEvaluationInteractor evaluationInteractor)
        {
            _evaluationInteractor = evaluationInteractor;
        }

        public EvaluationService()
        {

        }

        public EvaluationDomainModel CreateNewEvaluation(CreateEvaluationDomainModel createModel)
        {
            var evaluationModel = new EvaluationDomainModel();
            var newEvaluationEntity = ConvertModelToEntity(createModel);

            try
            {
                var evaluationId = EvaluationInteractor.CreateEvaluation(newEvaluationEntity);

                evaluationModel = GetEvaluation(evaluationId);
            }
            catch (Exception e)
            {
                // pass the exception to the controller as an error model

                // TO DO: Create ErrorModel

                throw; // stand-in till ErrorModel is figured out
            }

            return evaluationModel;

        }

        /// <summary>
        /// Gets the course attached to the passed ID, and displays the data currently in the database
        /// </summary>
        /// <param name="evaluationId"></param>
        /// <returns></returns>
        public EvaluationDomainModel GetEvaluation(Guid evaluationId)
        {
            var evaluationModel = new EvaluationDomainModel();

            try
            {
                var evaluationEntity = EvaluationInteractor.GetEvaluation(evaluationId);

                evaluationModel = new EvaluationDomainModel(evaluationEntity);
            }
            catch (Exception e)
            {
                // pass the exception to the controller as an error model

                // TO DO: Create ErrorModel

                throw; // stand-in till ErrorModel is figured out
            }
            
            return evaluationModel;
        }
        
        private static EvaluationEntity ConvertModelToEntity(CreateEvaluationDomainModel createModel)
        {
            return new EvaluationEntity
            {
                Name = createModel.Name,
                CourseId = createModel.CourseId,
                WeightId = createModel.WeightId,
                NumberOfScores = createModel.NumberOfScores,
                DropLowest = createModel.DropLowest
            };
        }
    }
}