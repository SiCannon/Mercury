using System;

namespace Memphis.Website.Dto
{
    public class AlbumListItemDto
    {
        public int? AlbumId { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public int? Year { get; set; }
        public int? Top3kPosition { get; set; }
        public Guid? MusicBrainzReleaseGroupId { get; set; }
        public bool HasThumbnail { get; set; }
    }
}