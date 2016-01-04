using System;

namespace Memphis.BusinessLogic.Dto
{
    public class AlbumSaveDto
    {
        public int? AlbumId { get; set; }
        public Guid? MusicBrainzReleaseGroupId { get; set; }
        public string Title { get; set; }
        public int? ArtistId { get; set; }
        public string Barcode { get; set; }
        public int? Year { get; set; }
        public int? Top3kPosition { get; set; }
    }
}
