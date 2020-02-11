using Common.Database.Data;
using Common.Database.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Common.Data
{
    public class ListRepository<T> : IRepository<T> where T : BaseEntity
    {
        private bool _disposed;

        public IEnumerable<T> GetAll()
        {
            return ListHelper<T>.ContextList;
        }

        public T GetById(int id)
        {
            return FindById(id) ?? throw new NullReferenceException(nameof(T));
        }

        public T FindById(int id)
        {
            return ListHelper<T>.ContextList.FirstOrDefault(x => x.Id == id);
        }

        public T Create(T entity)
        {
            if (entity == null) throw new ArgumentException(nameof(entity));

            ListHelper<T>.ContextList.Add(entity);
            return entity;
        }

        public T Update(T entity)
        {
            if (entity == null) { throw new ArgumentNullException(nameof(entity)); }

            ListHelper<T>.ContextList[ListHelper<T>.ContextList.FindIndex(x => x.Id == entity.Id)] = entity;
            return entity;
        }

        public void Delete(int entityId)
        {
            ListHelper<T>.ContextList.RemoveAll(x => x.Id == entityId);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ListHelper<T>.ContextList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
            if (disposing) ListHelper<T>.ContextList.Clear();
        }

        #endregion
    }
}
