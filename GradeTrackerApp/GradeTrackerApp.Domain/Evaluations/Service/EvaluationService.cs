using System;
using System.Collections.Generic;
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

        public IDomainModel CreateNewEvaluation(CreateEvaluationDomainModel createModel)
        {
            var evaluationModel = new EvaluationDomainModel();
            var newEvaluationEntity = ConvertModelToEntity(createModel);

            try
            {
                var evaluationId = EvaluationInteractor.CreateEvaluation(newEvaluationEntity);

                evaluationModel = (EvaluationDomainModel)GetEvaluation(evaluationId);
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
        public IDomainModel GetEvaluation(Guid evaluationId)
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

        public List<EvaluationDomainModel> GetEvaluationsForCourse(Guid courseId)
        {
            var evaluationEntities = EvaluationInteractor.GetByCourseId(courseId);

            return ConvertToDomainModel(evaluationEntities);
        }

        protected List<EvaluationDomainModel> ConvertToDomainModel(List<EvaluationEntity> entities)
        {
            var modelList = new List<EvaluationDomainModel>();

            foreach (var eval in entities)
            {
                modelList.Add(new EvaluationDomainModel(eval));
            }

            return modelList;
        }

        private static EvaluationEntity ConvertModelToEntity(CreateEvaluationDomainModel createModel)
        {
            return new EvaluationEntity
            {
                Name = createModel.Name,
                CourseId = createModel.CourseId,
                Weight = createModel.Weight,
                NumberOfScores = createModel.NumberOfScores,
                DropLowest = createModel.DropLowest
            };
        }
    }
}