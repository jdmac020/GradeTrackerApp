using System.Data.Entity;
using GradeTrackerApp.EntityFramework.Contexts;

namespace GradeTrackerApp.EntityFramework.UnitOfWork
{

    public class EfUnitOfWork : IEfUnitOfWork
    {
        public DbContext Context
        {
            get { return _context ?? (_context = new GradeTrackerDbContext()); }
            set { _context = value; }
        }

        private DbContext _context;

        public bool LazyLoadingEnabled
        {
            get { return Context.Configuration.LazyLoadingEnabled; }
            set { Context.Configuration.LazyLoadingEnabled = value; }
        }

        public EfUnitOfWork()
        {

        }

        public EfUnitOfWork(DbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
