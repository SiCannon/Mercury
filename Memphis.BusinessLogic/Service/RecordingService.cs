using System;
using System.Collections.Generic;
using System.Linq;
using Memphis.BusinessLogic.Base;
using Memphis.BusinessLogic.Interface;
using Memphis.Database.Entity;
using Memphis.Database.Infrastructure;

namespace Memphis.BusinessLogic.Service
{
    public class RecordingService : BaseService, IRecordingService
    {
        public RecordingService(IUnitOfWork work) : base(work)
        {

        }

        public IEnumerable<Recording> Query(int pageNumber, int pageSize, string searchField, string searchText, string sortBy, bool sortAsending)
        {
            var query = work.Recordings.AsQueryable();
            ApplyOrder(ref query, sortBy, sortAsending);
            ApplyFilter(ref query, searchField, searchText);
            return query
                .Skip(pageNumber * pageSize)
                .Take(pageSize);
        }

        public Recording GetById(int id)
        {
            return work.Recordings.Single(x => x.RecordingId == id);
        }

        public Recording GetByMusicBrainzId(Guid id)
        {
            return work.Recordings.SingleOrDefault(x => x.MusicBrainzId == id);
        }

        public void Save(Recording recording)
        {
            InternalSave(recording);
        }

        private void ApplyOrder(ref IQueryable<Recording> query, string sortBy, bool sortAsending)
        {
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
        }

        private void ApplyFilter(ref IQueryable<Recording> query, string searchField, string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                if (searchField == "title")
                {
                    query = query.Where(x => x.Title.Contains(searchText));
                }
            }
        }
    }
}
