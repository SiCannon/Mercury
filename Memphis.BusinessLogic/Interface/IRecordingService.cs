using System;
using System.Collections.Generic;
using Memphis.Database.Entity;

namespace Memphis.BusinessLogic.Interface
{
    public interface IRecordingService
    {
        IEnumerable<Recording> Query(int pageNumber, int pageSize, string searchField, string searchText, string sortBy, bool sortAsending);
        Recording GetById(int id);
        Recording GetByMusicBrainzId(Guid id);
        void Save(Recording recording);
    }
}
