using System;
using System.Collections.Generic;

namespace Memphis.Website.Dto
{
    public class AlbumDetailDto_OLD
    {
        public int? AlbumId { get; set; }
        public Guid? MusicBrainzReleaseGroupId { get; set; }
        public string Title { get; set; }
        public int? ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string Barcode { get; set; }
        public int? Year { get; set; }
        public int? Top3kPosition { get; set; }
        public bool HasThumbnail { get; set; }
        public List<AlbumDetailTrackDto> Tracks { get; set; }
    }
}