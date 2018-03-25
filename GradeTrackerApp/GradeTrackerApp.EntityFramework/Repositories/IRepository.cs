using System.Collections.Generic;
using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.EntityFramework.UnitOfWork;

namespace GradeTrackerApp.EntityFramework.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        IUnitOfWork UnitOfWork { get; set; }

        TEntity GetById(TKey id);
        TKey Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IList<TEntity> GetAll();
    }
}