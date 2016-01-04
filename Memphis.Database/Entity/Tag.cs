using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Memphis.Database.Interface;

namespace Memphis.Database.Entity
{
    public class Tag : IIsNew
    {
        public int? TagId { get; set; }

        [MaxLength(50)]
        [Index(IsUnique=true)]
        public string Name { get; set; }

        public virtual ICollection<ArtistTag> ArtistTags { get; set; }

        public bool IsNew
        {
            get { return !TagId.HasValue; }
        }
    }
}
