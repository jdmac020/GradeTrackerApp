using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.EntityFramework.UnitOfWork.EfUnitOfWork;

namespace GradeTrackerApp.EntityFramework.Repositories.EfRepository
{
    public interface IEfRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : IEntity<TKey>
    {
        IEfUnitOfWork EfUnitOfWork { get; set; }
    }
}