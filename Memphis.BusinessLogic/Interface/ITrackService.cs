using Memphis.Database.Entity;

namespace Memphis.BusinessLogic.Interface
{
    public interface ITrackService
    {
        Track GetByAlbumIdAndPosition(int albumId, int position);
        void Save(Track track); 
    }
}
