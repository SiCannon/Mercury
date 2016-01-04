using System;
using System.Linq;
using Memphis.BusinessLogic.Base;
using Memphis.BusinessLogic.Interface;
using Memphis.Database.Entity;
using Memphis.Database.Infrastructure;

namespace Memphis.BusinessLogic.Service
{
    public class TrackService : BaseService, ITrackService
    {
        public TrackService(IUnitOfWork work) : base(work)
        {

        }

        public Track GetByAlbumIdAndPosition(int albumId, int position)
        {
            return work.Tracks.SingleOrDefault(x => x.AlbumId == albumId && x.Position == position);
        }

        public void Save(Track track)
        {
            InternalSave(track);
        }
    }
}
