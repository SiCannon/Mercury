using System.Collections.Generic;
using System.Linq;
using Memphis.Database.Entity;
using Memphis.Website.Dto;

namespace Memphis.Website.Mapping
{
    public class ArtistDtoToArtistMapper
    {
        public static void Map(ArtistDto source, Artist target)
        {
            target.ArtistId = source.ArtistId;
            target.Name = source.Name;
            target.SortName = source.SortName;
            target.MusicBrainzId = source.MusicBrainzId;
        }

        public static Artist Map(ArtistDto source)
        {
            var result = new Artist();
            Map(source, result);
            return result;
        }

        public static ArtistTag MapTag(ArtistTagDto source)
        {
            return new ArtistTag
            {
                ArtistTagId = source.ArtistTagId,
                Count = source.Count,
                Tag = new Tag
                {
                    TagId = source.Tag.TagId,
                    Name = source.Tag.Name
                }
            };
        }

        public static IEnumerable<ArtistTag> MapTags(IEnumerable<ArtistTagDto> sources)
        {
            if (sources != null)
            {
                return sources.Select(x => MapTag(x));
            }
            else
            {
                return null;
            }
        }
    }
}