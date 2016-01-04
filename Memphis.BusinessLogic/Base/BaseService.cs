using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memphis.Database.Infrastructure;
using Memphis.Database.Interface;

namespace Memphis.BusinessLogic.Base
{
    public class BaseService
    {
        protected IUnitOfWork work;

        public BaseService(IUnitOfWork work)
        {
            this.work = work;
        }

        protected TEntity InternalSave<TEntity>(TEntity entity, bool commit = true) where TEntity : class, IIsNew
        {
            if (entity.IsNew)
            {
                work.GetDbSet<TEntity>().Add(entity);
            }
            else
            {
                var dbSet = work.GetDbSet<TEntity>();
                dbSet.Attach(entity);
                work.Modified(entity);
            }
            if (commit)
            {
                work.SaveChanges();
            }
            return entity;
        }
    }
}
