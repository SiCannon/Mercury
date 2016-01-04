using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Memphis.Database.Interface;

namespace Memphis.Database.Entity
{
    public class Artist : IIsNew
    {
        public int? ArtistId { get; set; }
        
        public string Name { get; set; }
        
        public string SortName { get; set; }

        [Index]
        public Guid? MusicBrainzId { get; set; }

        public virtual ICollection<ArtistTag> ArtistTags { get; set; }
        
        public virtual ICollection<Album> Albums { get; set; }

        public bool IsNew
        {
            get { return !ArtistId.HasValue; }
        }
    }
}
