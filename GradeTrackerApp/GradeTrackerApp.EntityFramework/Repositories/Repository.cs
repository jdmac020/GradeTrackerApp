using GradeTrackerApp.Core.Entities;
using GradeTrackerApp.EntityFramework.Repositories.EfRepository;

namespace GradeTrackerApp.EntityFramework.Repositories
{
    public class Repository<TEntity, TKey> : EfRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
    }
}