using System.Collections.Generic;
using Memphis.Database.Entity;
using System;

namespace Memphis.BusinessLogic.Interface
{
    public interface IArtistService
    {
        IEnumerable<Artist> GetAll();
        IEnumerable<Artist> Query(int pageNumber, int pageSize, string searchText, string sortBy, bool sortAsending);
        int Count(string searchText);
        Artist GetById(int id);
        Artist GetByMusicBrainzId(Guid mbzId);
        void Save(Artist artist);
        void SaveTags(Artist artist, IEnumerable<ArtistTag> add, IEnumerable<ArtistTag> edit, IEnumerable<ArtistTag> delete);
        void Delete(int id);
    }
}
