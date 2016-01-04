using System.ComponentModel.DataAnnotations.Schema;
using Memphis.Database.Interface;

namespace Memphis.Database.Entity
{
    public class Track : IIsNew
    {
        public int? TrackId { get; set; }

        [Index("ixAlbumPosition", 1, IsUnique = true)]
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }

        [Index("ixAlbumPosition", 2, IsUnique = true)]
        public int Position { get; set; }

        public int RecordingId { get; set; }
        public virtual Recording Recording { get; set; }

        public bool IsNew
        {
            get
            {
                return !TrackId.HasValue;
            }
        }
    }
}
