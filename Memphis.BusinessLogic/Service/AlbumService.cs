using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Memphis.BusinessLogic.Base;
using Memphis.BusinessLogic.Dto;
using Memphis.BusinessLogic.Interface;
using Memphis.Database.Entity;
using Memphis.Database.Infrastructure;

namespace Memphis.BusinessLogic.Service
{
    public class AlbumService : BaseService, IAlbumService
    {
        IMappingEngine mapper;

        public AlbumService(IUnitOfWork work, IMappingEngine mapper) : base(work)
        {
            this.mapper = mapper;
        }

        public IEnumerable<Album> Query(int pageNumber, int pageSize, string searchField, string searchText, string sortBy, bool sortAsending)
        {
            var query = work.Albums.Include(x => x.Artist).AsQueryable();

            ApplyFilter(ref query, searchField, searchText);

            if (sortBy == "title")
            {
                if (sortAsending)
                {
                    query = query.OrderBy(x => x.Title);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Title);
                }
            }
            else if (sortBy == "artist")
            {
                if (sortAsending)
                {
                    query = query.OrderBy(x => x.Artist.Name);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Artist.Name);
                }
            }
            else if (sortBy == "t3kposition")
            {
                if (sortAsending)
                {
                    query = query.OrderBy(x => x.Top3kPosition ?? 1000000);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Top3kPosition);
                }
            }

            return query
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }

        public int Count(string searchField, string searchText)
        {
            var query = work.Albums.AsQueryable();

            ApplyFilter(ref query, searchField, searchText);

            return query.Count();
        }

        public Album GetById(int id)
        {
            return work.Albums.Include(x => x.Artist).SingleOrDefault(x => x.AlbumId == id);
        }

        public Album GetByMusicBrainzId(Guid id)
        {
            return work.Albums.SingleOrDefault(x => x.MusicBrainzReleaseGroupId == id);
        }

        public void Save(Album album)
        {
            InternalSave(album);
        }

        public void Save(AlbumSaveDto album)
        {
            var dbAlbum = work.Albums.SingleOrDefault(x => x.AlbumId == album.AlbumId);
            if (dbAlbum == null)
            {
                dbAlbum = new Album();
                work.Albums.Add(dbAlbum);
            }
            mapper.Map(album, dbAlbum);
            work.SaveChanges();
            album.AlbumId = dbAlbum.AlbumId;
        }

        private void ApplyFilter(ref IQueryable<Album> query, string searchField, string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                if (searchField == "title")
                {
                    query = query.Where(a => a.Title.Contains(searchText));
                }
                else if (searchField == "artist")
                {
                    query = query.Where(a => a.Artist.Name.Contains(searchText));
                }
            }
        }


        public IEnumerable<Album> GetAll(bool includeArtists = false)
        {
            if (includeArtists)
            {
                return work.Albums.Include(x => x.Artist);
            }
            else
            {
                return work.Albums;
            }
        }
    }
}
