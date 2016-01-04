using System;

namespace Memphis.Website.Dto
{
    public class ArtistTagDto
    {
        public int? ArtistTagId { get; set; }
        public TagDto Tag { get; set; }
        public int Count { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEdited { get; set; }
    }
}