using Common.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Database.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        void SaveChanges();
    }
}
