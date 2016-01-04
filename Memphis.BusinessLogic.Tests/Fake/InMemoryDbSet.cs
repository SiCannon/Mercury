using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Memphis.BusinessLogic.Tests.Fake
{
    public class InMemoryDbSet<T> : IDbSet<T> where T : class
    {
        public InMemoryDbSet(IEnumerable<T> entities)
        {
            set = new HashSet<T>();
            foreach (var e in entities)
            {
                set.Add(e);
            }
            qset = set.AsQueryable();
        }

        public InMemoryDbSet() : this(Enumerable.Empty<T>())
        {

        }

        public T Add(T entity)
        {            
            set.Add(entity);
            return entity;
        }

        public T Attach(T entity)
        {
            set.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> Local
        {
            get { throw new NotImplementedException(); }
        }

        public T Remove(T entity)
        {
            set.Remove(entity);
            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType
        {
            get { return qset.ElementType; }
        }

        public Expression Expression
        {
            get { return qset.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return qset.Provider; }
        }

        readonly HashSet<T> set;
        readonly IQueryable<T> qset;
    }
}
