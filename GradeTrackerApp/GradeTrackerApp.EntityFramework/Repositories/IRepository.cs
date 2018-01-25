using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.EntityFramework.UnitOfWork;

namespace GradeTrackerApp.EntityFramework.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : IEntity<TKey>
    {
        IUnitOfWork UnitOfWork { get; set; }

        TEntity GetById(TKey id);
        TEntity Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IList<TEntity> GetAll();
    }
}