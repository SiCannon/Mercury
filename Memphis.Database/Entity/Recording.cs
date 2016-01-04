using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Memphis.Database.Interface;

namespace Memphis.Database.Entity
{
    public class Recording : IIsNew
    {
        public int? RecordingId { get; set; }
        public string Title { get; set; }
        public int Length { get; set; }

        [Index]
        public Guid? MusicBrainzId { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }

        public bool IsNew
        {
            get
            {
                return !RecordingId.HasValue;
            }
        }
    }
}
