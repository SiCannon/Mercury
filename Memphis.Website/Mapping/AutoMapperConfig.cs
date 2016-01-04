using AutoMapper;
using Memphis.BusinessLogic.Dto;
using Memphis.Database.Entity;
using Memphis.Website.Dto;
using Memphis.Website.ViewModel;

namespace Memphis.Website.Mapping
{
    public class AutoMapperConfig
    {
        public static void RegisterAutoMaps()
        {
            Mapper.CreateMap<Artist, ArtistViewModel>();
            Mapper.CreateMap<Artist, ArtistDto>();
            Mapper.CreateMap<Artist, ArtistListItemDto>();
            Mapper.CreateMap<Tag, TagDto>();
            Mapper.CreateMap<ArtistTag, ArtistTagDto>()
                .ForMember(d => d.IsNew, s => s.UseValue(false))
                .ForMember(d => d.IsEdited, s => s.UseValue(false))
                .ForMember(d => d.IsDeleted, s => s.UseValue(false));

            Mapper.CreateMap<Album, AlbumListItemDto>();
            Mapper.CreateMap<Album, AlbumDetailDto_OLD>();

            Mapper.CreateMap<AlbumSaveDto, Album>()
                //.ForMember(d => d.AlbumId, s => s.Ignore())
                .ForMember(d => d.Artist, s => s.Ignore())
                .ForMember(d => d.HasThumbnail, s => s.Ignore())
                .ForMember(d => d.Tracks, s => s.Ignore());

            Mapper.CreateMap<Track, AlbumDetailTrackDto>();

            Mapper.CreateMap<Recording, RecordingSelectItemDto>();

            Mapper.AssertConfigurationIsValid();
        }
    }
}