using System;
using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.Core.Exceptions;
using GradeTrackerApp.Domain.Semesters.Models;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Interactors.Semester;

namespace GradeTrackerApp.Domain.Semesters.Service
{
    public class SemesterService : ISemesterService
    {
        #region Interactors

        public ISemesterInteractor Interactor
        {
            get { return _interactor ?? (_interactor = new SemesterInteractor());}
            set { _interactor = value; }
        }

        private ISemesterInteractor _interactor;

        #endregion

        public SemesterService() { }

        public SemesterService(ISemesterInteractor interactor)
        {
            _interactor = interactor;
        }

        public IDomainModel GetSemester(Guid semesterId)
        {
            var entity = new SemesterEntity();

            try
            {
                entity = Interactor.GetSemester(semesterId);
            }
            catch (GradeTrackerException gte)
            {
                return new ErrorDomainModel(gte, false);
            }
            

            return ConvertEntityToModel(entity);
        }

        protected SemesterDomainModel ConvertEntityToModel(IEntity<Guid> entity)
        {
            return new SemesterDomainModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public List<IDomainModel> GetAllSemesters()
        {
            var semesterModelList = new List<IDomainModel>();
            var semesters = new List<SemesterEntity>();

            try
            {
                semesters = Interactor.GetAllSemesters();
            }
            catch (GradeTrackerException gte)
            {
                return new List<IDomainModel> {new ErrorDomainModel(gte, false)};
            }
            

            foreach (var semesterEntity in semesters)
            {
                semesterModelList.Add(ConvertEntityToModel(semesterEntity));
            }

            return semesterModelList;
        }
    }
}