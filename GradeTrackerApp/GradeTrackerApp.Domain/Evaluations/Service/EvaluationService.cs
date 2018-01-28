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
            var newCourseEntity = ConvertModelToEntity(createModel);

            var courseId = EvaluationInteractor.CreateEvaluation(newCourseEntity);

            var courseModel = GetEvaluation(courseId);

            return courseModel;

        }

        /// <summary>
        /// Gets the course attached to the passed ID, and displays the data currently in the database
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public EvaluationDomainModel GetEvaluation(Guid courseId)
        {
            var courseEntity = EvaluationInteractor.GetEvaluationById(courseId);

            var courseModel = new EvaluationDomainModel(courseEntity);

            return courseModel;
        }



        private EvaluationEntity ConvertModelToEntity(CreateEvaluationDomainModel createModel)
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