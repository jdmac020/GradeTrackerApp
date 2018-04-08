using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Domain.Shared;
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

            var evaluationId = Guid.Empty;

            try
            {
                evaluationId = EvaluationInteractor.CreateEvaluation(newEvaluationEntity);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, true);
            }

            try
            {
                evaluationModel = (EvaluationDomainModel)GetEvaluation(evaluationId);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, false);
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
            var evaluationEntity = new EvaluationEntity();

            try
            {
                evaluationEntity = EvaluationInteractor.GetEvaluation(evaluationId);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, false);
            }

            evaluationModel = new EvaluationDomainModel(evaluationEntity);

            return evaluationModel;
        }

        public List<IDomainModel> GetEvaluationsForCourse(Guid courseId)
        {
            var entityList = new List<EvaluationEntity>();

            try
            {
                entityList = EvaluationInteractor.GetByCourseId(courseId);
            }
            catch (GradeTrackerException gte)
            {
                return new List<IDomainModel> { new ErrorDomainModel(gte, false)};
            }

            entityList = EvaluationInteractor.GetByCourseId(courseId);

            return ConvertToDomainModel(entityList);
        }

        protected List<IDomainModel> ConvertToDomainModel(List<EvaluationEntity> entities)
        {
            var modelList = new List<IDomainModel>();

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