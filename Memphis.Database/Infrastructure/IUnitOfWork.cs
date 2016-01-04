using System;
using System.Data.Entity;
using Memphis.Database.Entity;

namespace Memphis.Database.Infrastructure
{
    public interface IUnitOfWork
    {
        IDbSet<Album> Albums { get; }
        IDbSet<Artist> Artists { get; }
        IDbSet<ArtistTag> ArtistTags { get; }
        IDbSet<Recording> Recordings { get; }
        IDbSet<Tag> Tags { get; }
        IDbSet<Track> Tracks { get; }

        int SaveChanges();
        void Added<TEntity>(TEntity entity) where TEntity : class;
        void Modified<TEntity>(TEntity entity) where TEntity : class;
        void Unchanged<TEntity>(TEntity entity) where TEntity : class;
        void Deleted<TEntity>(TEntity entity) where TEntity : class;
        IDbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;
        void RefreshFromDatabase(object entity);
        bool IsAttached<TEntity>(TEntity entity, Func<TEntity, bool> predicate) where TEntity : class;
        void Attach<TEntity>(TEntity entity) where TEntity : class;
        void Detach(object entity);
        void SetValues(object dbEntity, object sourceEntity);
    }
}
