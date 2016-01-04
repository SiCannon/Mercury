using System;
using System.Collections.Generic;

namespace Memphis.Website.Dto
{
    public class ArtistDto
    {
        public int? ArtistId { get; set; }
        public string Name { get; set; }
        public string SortName { get; set; }
        public Guid? MusicBrainzId { get; set; }
        public List<ArtistTagDto> ArtistTags { get; set; }
    }
}