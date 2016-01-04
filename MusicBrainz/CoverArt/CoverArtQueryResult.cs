using System.Collections.Generic;

namespace MusicBrainz.CoverArt
{
    public class CoverArtQueryResult
    {
        public List<CoverArtImageData> images { get; set; }
        public string release { get; set; }

        public string SmallThumbnailUrl
        {
            get
            {
                if (images.Count > 0 && images[0].thumbnails != null && !string.IsNullOrEmpty(images[0].thumbnails.small))
                {
                    return images[0].thumbnails.small;
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public class CoverArtImageData
    {
        public bool approved { get; set; }
        public bool back { get; set; }
        public string comment { get; set; }
        public int edit { get; set; }
        public bool front { get; set; }
        public string id { get; set; }
        public string image { get; set; }
        public CoverArtImageThumbnails thumbnails { get; set; }
        public List<string> types { get; set; }
    }

    public class CoverArtImageThumbnails
    {
        public string large { get; set; }
        public string small { get; set; }
    }

        
}
