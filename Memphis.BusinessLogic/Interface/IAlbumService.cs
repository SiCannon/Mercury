using System;
using System.Collections.Generic;
using Memphis.BusinessLogic.Dto;
using Memphis.Database.Entity;

namespace Memphis.BusinessLogic.Interface
{
    public interface IAlbumService
    {
        IEnumerable<Album> Query(int pageNumber, int pageSize, string searchField, string searchText, string sortBy, bool sortAsending);
        int Count(string searchField, string searchText);
        Album GetById(int id);
        Album GetByMusicBrainzId(Guid id);
        IEnumerable<Album> GetAll(bool includeArtists = false);
        void Save(Album album);
        void Save(AlbumSaveDto album);
    }
}
