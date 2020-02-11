using Common.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Database.Data
{
    public interface IRepository<TEntity> : IEnumerable<TEntity>, IDisposable
        where TEntity : BaseEntity
    {
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(int entityId);
        TEntity GetById(int id);
        TEntity FindById(int id);
    }
}
