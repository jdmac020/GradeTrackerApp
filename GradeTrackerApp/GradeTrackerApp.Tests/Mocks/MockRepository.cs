using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.EntityFramework.Entities;
using GradeTrackerApp.EntityFramework.Repositories;

namespace GradeTrackerApp.Tests.Mocks
{
    public class MockRepository<TEntity,TKey> : Repository<TEntity,TKey> where TEntity : class, IEntity<TKey>
    {
        private List<TEntity> _dataSet;

        public MockRepository()
        {
            _dataSet = new List<TEntity>();
        }

        public new TEntity GetById(TKey id)
        {
            return _dataSet.Find(e => e.Id.Equals(id));
        }

        public new TEntity Create(TEntity entity)
        {
            _dataSet.Add(entity);

            return _dataSet.LastOrDefault();
        }

        public new void Update(TEntity entity)
        {
            _dataSet.Remove(entity);
            _dataSet.Add(entity);
        }

        public new void Delete(TEntity entity)
        {
            _dataSet.Remove(entity);
        }

        public new IList<TEntity> GetAll()
        {
            return _dataSet;
        }
    }
}
