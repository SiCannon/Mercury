using System;
using System.Xml.Serialization;

namespace Top3000Albums.Entity
{
    [Serializable]
    public class Album
    {
        [XmlAttribute]
        public string AlbumId { get; set; }

        [XmlAttribute]
        public string Place { get; set; }

        [XmlAttribute]
        public string Artist { get; set; }

        [XmlAttribute]
        public string Title { get; set; }

        [XmlAttribute]
        public string MbzArtistId { get; set; }

        [XmlAttribute]
        public string MbzReleaseGroupId { get; set; }

        #region typed nullable fields

        [XmlIgnore]
        public int? AlbumIdAsInteger
        {
            get
            {
                if (string.IsNullOrEmpty(AlbumId))
                    return null;
                else
                    return Convert.ToInt32(AlbumId);
            }
            set
            {
                AlbumId = value.ToString();
            }
        }

        [XmlIgnore]
        public Guid? MbzArtistIdAsGuid
        {
            get
            {
                if (string.IsNullOrEmpty(MbzArtistId))
                    return null;
                else
                    return new Guid(MbzArtistId);
            }
            set
            {
                MbzArtistId = value.ToString();
            }
        }

        [XmlIgnore]
        public Guid? MbzReleaseGroupIdAsGuid
        {
            get
            {
                if (string.IsNullOrEmpty(MbzReleaseGroupId))
                    return null;
                else
                    return new Guid(MbzReleaseGroupId);
            }
            set
            {
                MbzReleaseGroupId = value.ToString();
            }
        }

        [XmlIgnore]
        public int? PlaceAsInt
        {
            get
            {
                if (string.IsNullOrEmpty(Place))
                    return null;
                else
                    return Convert.ToInt32(Place);
            }
        }

        #endregion

        [XmlIgnore]
        public bool HasMbzArtist
        {
            get
            {
                return !string.IsNullOrEmpty(MbzArtistId);
            }
        }

        [XmlIgnore]
        public bool HasMbzReleaseGroup
        {
            get
            {
                return !string.IsNullOrEmpty(MbzReleaseGroupId);
            }
        }

    }
}
