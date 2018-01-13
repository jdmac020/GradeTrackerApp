using System;

namespace GradeTrackerApp.EntityFramework.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        bool LazyLoadingEnabled { get; set; }

        void Commit();
    }
}