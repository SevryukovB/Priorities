using Common.Database.Data;
using Common.Database.Models;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private bool _disposed;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return _serviceProvider.GetRequiredService<IRepository<T>>();
        }

        public void SaveChanges()
        {
            //Context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
            if (disposing)
            {
                //Context?.Dispose();  
            }
        }
    }
}
