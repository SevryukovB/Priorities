using Common.Database.Data;
using Common.Database.Models;
using Common.Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Services
{
    public class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> Repository;
        //protected IList<T> ExtendedRepositoryQueryable { get; set; }
        private bool _disposed;
        protected IUnitOfWork UnitOfWork;

        public EntityService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Repository = unitOfWork.GetRepository<T>();
            //ExtendedRepositoryQueryable = Repository.ToList();
        }

        public virtual T Create(T entity)
        {
            entity = Repository.Create(entity);
            UnitOfWork.SaveChanges();

            return entity;
        }

        public virtual void Update(T entity)
        {
            Repository.Update(entity);
            UnitOfWork.SaveChanges();
        }

        public virtual void Delete(int entityId)
        {
            Repository.Delete(entityId);
            UnitOfWork.SaveChanges();
        }

        public T FindById(int id)
        {
            return Repository.FirstOrDefault(entity => entity.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return Repository;
        }

        public IEnumerable<T> GetAll(int pageIndex, int pageSize)
        {
            return Repository
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);
        }

        public T GetById(int id)
        {
            return Repository.FirstOrDefault(entity => entity.Id == id)
                   ?? throw new KeyNotFoundException($"Entity {typeof(T)} by Id={id} has not been found");
        }

        #region IDisposable implementation 

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
            if (disposing) Repository?.Dispose();
        }

        #endregion
    }
}
