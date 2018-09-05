using System;
using System.Collections.Generic;
using System.Linq;
using ConjureGrade.Spells;
using ConjureGrade.Wizards;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Courses.Models;
using GradeTrackerApp.Domain.Evaluations.Models;
using GradeTrackerApp.Domain.Evaluations.Service;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Interactors.Course;

namespace GradeTrackerApp.Domain.Courses.Service
{
    public class CourseService : ICourseService
    {
        #region Services

        public IEvaluationService EvaluationService
        {
            get { return _evaluationService ?? (_evaluationService = new EvaluationService()); }
            set { _evaluationService = value; }
        }

        private IEvaluationService _evaluationService;

        #endregion

        #region Interactors


        public ICourseInteractor CourseInteractor
        {
            get { return _courseInteractor ?? (_courseInteractor = new CourseInteractor()); }
            set { _courseInteractor = value; }
        }

        private ICourseInteractor _courseInteractor;

        #endregion

        /// <summary>
        /// Constructor to override default interactor
        /// </summary>
        /// <param name="courseInteractor">Interactor to be used by class</param>
        public CourseService(ICourseInteractor courseInteractor)
        {
            _courseInteractor = courseInteractor;
        }
        
        public CourseService()
        {

        }

        public IDomainModel CreateCourse(CreateOrEditCourseDomainModel createModel)
        {
            var newCourseEntity = ConvertModelToEntity(createModel);

            var courseId = Guid.Empty;
            var courseModel = new CourseDomainModel();

            try
            {
                courseId = CourseInteractor.CreateCourse(newCourseEntity);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, true);
            }

            try
            {
                courseModel = (CourseDomainModel)GetCourse(courseId);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, false);
            }
            
            return courseModel;

        }

        /// <summary>
        /// Gets the course attached to the passed ID, and displays the data currently in the database
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public IDomainModel GetCourse(Guid courseId)
        {
            var courseEntity = new CourseEntity();

            try
            {
                courseEntity = CourseInteractor.GetCourse(courseId);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, false);
            }

            var courseModel = new CourseDomainModel(courseEntity);

            return courseModel;
        }

        public IDomainModel DeleteCourse(Guid courseId)
        {
            var deletedCourseModel = new CourseDomainModel();

            try
            {
                var course = CourseInteractor.GetCourse(courseId);

                var linkedEvaluations = EvaluationService.GetEvaluationsForCourse(courseId);

                if (linkedEvaluations.Count > 0)
                {
                    foreach (var evaluation in linkedEvaluations)
                    {
                        var evaluationModel = (EvaluationDomainModel) evaluation;
                        EvaluationService.DeleteEvaluation(evaluationModel.Id);
                    }
                }

                CourseInteractor.DeleteCourse(courseId);

                deletedCourseModel = new CourseDomainModel { StudentId = course.StudentId };

            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, false);
            }

            return deletedCourseModel;
        }

        public IDomainModel UpdateCourse(CreateOrEditCourseDomainModel updatedModel)
        {
            var returnModel = new CourseDomainModel();

            try
            {
                var entityToUpdate = ConvertModelToEntity(updatedModel);
                CourseInteractor.UpdateCourse(entityToUpdate);

                var updatedEntity = CourseInteractor.GetCourse(entityToUpdate.Id);

                returnModel = new CourseDomainModel(updatedEntity);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, true);
            }

            return returnModel;
        }

        // Will eventually pass in a student identifier and get all the courses associated
        public List<IDomainModel> GetCourses(Guid userId)
        {
            var domainModels = new List<IDomainModel>();
            var courseEntities = new List<CourseEntity>();

            try
            {
                courseEntities = CourseInteractor.GetCoursesByStudentId(userId);
            }
            catch (GradeTrackerException gte)
            {
                return new List<IDomainModel> {new ErrorDomainModel(gte, false)};
            }

            foreach (var entity in courseEntities)
            {
                domainModels.Add(new CourseDomainModel(entity));
            }

            return domainModels;
        }

        public void EvaluationModified(Guid courseId)
        {
            var course = CourseInteractor.GetCourse(courseId);
            CalculateGrade(course);
            CourseInteractor.UpdateCourse(course);
        }

        private void CalculateGrade(CourseEntity course)
        {
            var courseWizard = new CourseWizard();
            var evalSpells = new List<EvaluationResult>();

            var evals = EvaluationService.GetEvaluationsForCourse(course.Id);

            foreach (var eval in evals)
            {
                var castedEval = (EvaluationDomainModel) eval;
                var weighted = castedEval.Weight != 1;

                evalSpells.Add(new EvaluationResult
                {
                    Weighted = weighted,
                    WeightAmount = castedEval.Weight,
                    PointsEarned = castedEval.PointsEarned,
                    GradeToDateRaw = castedEval.CurrentPointsGrade,
                    GradeOverallRaw = castedEval.FinalPointsGrade,
                    PointsPossibleOverall = castedEval.TotalPointsPossible,
                    PointsPossibleToDate = castedEval.CurrentPointsPossible
                });
            }

            ICourseResult courseSpell = null;

            if (evalSpells.Any(es => es.Weighted))
            {

                //foreach (var eval in evalSpells)
                //{
                //    eval.GradeOverallRaw =
                //        MathApprentice.CalculateRawPercentage(eval.PointsEarned, eval.PointsPossibleOverall);
                //    eval.GradeToDateRaw =
                //        MathApprentice.CalculateRawPercentage(eval.PointsEarned, eval.PointsPossibleToDate);
                //}

                courseSpell = new WeightedCourseResult
                {
                    Evaluations = evalSpells,
                    
                };

                courseWizard.Course = courseSpell;

                courseWizard.UpdateAllGrades();

                var courseResult = (WeightedCourseResult)courseWizard.Course;

                course.CurrentPointsGrade = courseResult.GradeToDateRaw;
                course.FinalPointsGrade = courseResult.GradeOverallRaw;

            }
            else
            {
                courseSpell = new CourseResult
                {
                    Evaluations = evalSpells
                };

                courseWizard.Course = courseSpell;

                courseWizard.UpdateAllGrades();

                var courseResult = (CourseResult)courseWizard.Course;

                course.PointsEarned = courseResult.PointsEarned;
                course.TotalPointsPossible = courseResult.PointsPossibleOverall;
                course.CurrentPointsPossible = courseResult.PointsPossibleToDate;
                course.CurrentPointsGrade = courseResult.GradeToDateRaw;
                course.FinalPointsGrade = courseResult.GradeOverallRaw;
            }

            
        }

        private static CourseEntity ConvertModelToEntity(CourseDomainModel updatedModel)
        {
            return new CourseEntity
            {
                Id = updatedModel.Id,
                StudentId = updatedModel.StudentId,
                Name = updatedModel.Name,
                Department = updatedModel.Department,
                Number = updatedModel.Number,
                //SchoolId = updatedModel.SchoolId,
                //InstructorId = updatedModel.InstructorId,
                Year = updatedModel.Year,
                SemesterId = updatedModel.SemesterId,
                //StartTime = updatedModel.StartTime,
                //EndTime = updatedModel.EndTime,
                //StartDate = updatedModel.StartDate,
                //EndDate = updatedModel.EndDate
            };
        }

        private static CourseEntity ConvertModelToEntity(CreateOrEditCourseDomainModel createModel)
        {
            return new CourseEntity
            {
                Id = createModel.Id ?? Guid.Empty,
                StudentId = createModel.StudentId,
                Name = createModel.Name,
                Department = createModel.Department,
                Number = createModel.Number,
                //SchoolId = updatedModel.SchoolId,
                //InstructorId = updatedModel.InstructorId,
                Year = createModel.Year,
                SemesterId = createModel.SemesterId,
                //StartTime = updatedModel.StartTime,
                //EndTime = updatedModel.EndTime,
                //StartDate = updatedModel.StartDate,
                //EndDate = updatedModel.EndDate
            };
        }

        public CourseWeightDomainModel GetCourseWeightType(Guid courseId)
        {
            var returnModel = new CourseWeightDomainModel();
            var evals = EvaluationService.GetEvaluationsForCourse(courseId);

            if (evals.Count > 0)
            {
                var evalModels = new List<EvaluationDomainModel>();

                foreach(var eval in evals)
                {
                    evalModels.Add((EvaluationDomainModel)eval);
                }

                returnModel.IsWeighted = evalModels.Any(e => e.Weight != 1);
                returnModel.IsStraightPoints = evalModels.Any(e => e.Weight == 1); 
            }

            return returnModel;
        }
    }
}