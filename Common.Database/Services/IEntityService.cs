using Common.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Database.Services
{
    public interface IEntityService<T> : IDisposable where T : BaseEntity
    {
        T Create(T entity);
        void Update(T entity);
        void Delete(int entityId);
        T GetById(int id);
        T FindById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(int pageIndex, int pageSize);
    }
}
