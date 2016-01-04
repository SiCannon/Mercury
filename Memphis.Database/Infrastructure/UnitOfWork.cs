using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Memphis.Database.Entity;

namespace Memphis.Database.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDbSet<Album> Albums { get { return db.Albums; } }
        public IDbSet<Artist> Artists { get { return db.Artists; } }
        public IDbSet<ArtistTag> ArtistTags { get { return db.ArtistTags; } }
        public IDbSet<Recording> Recordings { get { return db.Recordings; } }
        public IDbSet<Tag> Tags { get { return db.Tags; } }
        public IDbSet<Track> Tracks { get { return db.Tracks; } }

        private MemphisContext db;

        public UnitOfWork()
        {
            this.db = new MemphisContext();
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }

        public void Added<TEntity>(TEntity entity) where TEntity : class
        {
            db.Entry(entity).State = EntityState.Added;
        }

        public void Modified<TEntity>(TEntity entity) where TEntity : class
        {
            db.Entry(entity).State = EntityState.Modified;
        }

        public void Unchanged<TEntity>(TEntity entity) where TEntity : class
        {
            db.Entry(entity).State = EntityState.Unchanged;
        }

        public void Deleted<TEntity>(TEntity entity) where TEntity : class
        {
            db.Entry(entity).State = EntityState.Deleted;
        }

        public IDbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return db.Set<TEntity>();
        }

        public void RefreshFromDatabase(object entity)
        {
            ((IObjectContextAdapter)db).ObjectContext.Refresh(RefreshMode.StoreWins, entity);
        }

        public bool IsAttached<TEntity>(TEntity entity, Func<TEntity, bool> predicate) where TEntity : class
        {
            return GetDbSet<TEntity>().Any(predicate);
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            GetDbSet<TEntity>().Attach(entity);
        }

        public void Detach(object entity)
        {
            ((IObjectContextAdapter)db).ObjectContext.Detach(entity);
        }

        public void SetValues(object dbEntity, object sourceEntity)
        {
            db.Entry(dbEntity).CurrentValues.SetValues(sourceEntity);
        }
    }
}
