using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Memphis.BusinessLogic.Base;
using Memphis.BusinessLogic.Interface;
using Memphis.Database.Infrastructure;
using Memphis.Database.Entity;
using System;
using Memphis.BusinessLogic.Lib;

namespace Memphis.BusinessLogic.Service
{
    public class ArtistService : BaseService, IArtistService
    {
        ITagService tagService;

        public ArtistService(IUnitOfWork work, ITagService tagService) : base(work)
        {
            this.tagService = tagService;
        }

        public IEnumerable<Artist> GetAll()
        {
            return work.Artists;
        }


        public Artist GetByMusicBrainzId(Guid mbzId)
        {
            return work.Artists.SingleOrDefault(x => x.MusicBrainzId == mbzId);
        }


        public void Save(Artist artist)
        {
            InternalSave(artist);
        }


        public IEnumerable<Artist> Query(int pageNumber, int pageSize, string searchText, string sortBy, bool sortAsending)
        {
            var query = work.Artists.AsQueryable();

            ApplyFilter(ref query, searchText);

            var sorter = new Sorter<Artist>
            (
                new SorterSortName<Artist>("name", x => x.SortName),
                new SorterSortName<Artist>("mbzid", x => x.MusicBrainzId.ToString()),
                new SorterSortName<Artist>("id", x => x.ArtistId.ToString())
            );

            return sorter.Sort(query, sortBy, sortAsending)
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }


        public Artist GetById(int id)
        {
            return work.Artists
                .Include(x => x.ArtistTags)
                .SingleOrDefault(x => x.ArtistId == id);
        }


        public int Count(string searchText)
        {
            var query = work.Artists.AsQueryable();

            ApplyFilter(ref query, searchText);

            return query.Count();
        }

        private void ApplyFilter(ref IQueryable<Artist> query, string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(a => a.Name.Contains(searchText));
            }
        }


        public void Delete(int id)
        {
            var artist = work.Artists.SingleOrDefault(x => x.ArtistId == id);
            foreach (var artistTag in artist.ArtistTags.ToList())
            {
                work.ArtistTags.Remove(artistTag);
            }
            work.Artists.Remove(artist);
            work.SaveChanges();
        }


        public void SaveTags(Artist artist, IEnumerable<ArtistTag> add, IEnumerable<ArtistTag> edit, IEnumerable<ArtistTag> delete)
        {
            if (artist.ArtistTags == null)
            {
                artist.ArtistTags = new List<ArtistTag>();
            }

            if (add != null)
            {
                foreach (var artistTag in add)
                {
                    var tag = artistTag.Tag;
                    tagService.Save(ref tag, false);
                    artist.ArtistTags.Add(new ArtistTag { Tag = tag });
                }
            }

            if (edit != null)
            {
                foreach (var artistTag in edit)
                {
                    var tag = artistTag.Tag;
                    tagService.Save(ref tag, false);

                    var existingArtistTag = artist.ArtistTags.SingleOrDefault(x => x.ArtistTagId == artistTag.ArtistTagId);
                    if (existingArtistTag == null)
                    {
                        artist.ArtistTags.Add(new ArtistTag { Tag = tag });
                    }
                    else
                    {
                        existingArtistTag.Tag = tag;
                    }
                }
            }

            if (delete != null)
            {
                foreach (var artistTag in delete)
                {
                    var existingArtistTag = artist.ArtistTags.SingleOrDefault(x => x.ArtistTagId == artistTag.ArtistTagId);
                    if (existingArtistTag != null)
                    {
                        artist.ArtistTags.Remove(existingArtistTag);
                    }
                }
            }

            work.SaveChanges();
        }
    }
}
