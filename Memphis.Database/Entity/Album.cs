using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Memphis.Database.Interface;

namespace Memphis.Database.Entity
{
    public class Album : IIsNew
    {
        public int? AlbumId { get; set; }
        
        [Index]
        public Guid? MusicBrainzReleaseGroupId { get; set; }

        public string Title { get; set; }

        public int? ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        
        public string Barcode { get; set; }
        
        public int? Year { get; set; }
        
        public int? Top3kPosition { get; set; }
        
        public bool HasThumbnail { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }

        public bool IsNew
        {
            get { return !AlbumId.HasValue; }
        }
    }
}
