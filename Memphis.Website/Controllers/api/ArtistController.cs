using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Memphis.BusinessLogic.Interface;
using Memphis.Website.Dto;
using Memphis.Website.Mapping;

namespace Memphis.Website.Controllers.api
{
    [RoutePrefix("api/Artist")]
    public class ArtistController : ApiController
    {
        IMappingEngine mapper;
        IArtistService artistService;

        public ArtistController(IMappingEngine mapper, IArtistService artistService)
        {
            this.mapper = mapper;
            this.artistService = artistService;
        }
        
        [Route("")]
        public IEnumerable<ArtistListItemDto> Get(int pageNumber = 0, int pageSize = 20, string searchText = "", string sortBy = "name", bool sortAsending = true)
        {
            return mapper.Map<List<ArtistListItemDto>>(artistService.Query(pageNumber, pageSize, searchText, sortBy, sortAsending));
        }

        [Route("Count")]
        public int GetCount(string searchText = "")
        {
            return artistService.Count(searchText);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var artist = artistService.GetById(id);
            artist = null;
            if (artist != null)
            {
                return Ok(mapper.Map<ArtistDto>(artist));
            }
            else
            {
                return NotFound();
            }
        }

        [Route("")]
        public int Post([FromBody]ArtistDto artist)
        {
            if (artist.ArtistId.HasValue)
            {
                var existingArtist = artistService.GetById(artist.ArtistId.Value);
                ArtistDtoToArtistMapper.Map(artist, existingArtist);
                artistService.Save(existingArtist);
                artistService.SaveTags(existingArtist,
                    ArtistDtoToArtistMapper.MapTags(artist.ArtistTags.Where(x => x.IsNew && !x.IsDeleted)),
                    ArtistDtoToArtistMapper.MapTags(artist.ArtistTags.Where(x => x.IsEdited && !x.IsNew && !x.IsDeleted)),
                    ArtistDtoToArtistMapper.MapTags(artist.ArtistTags.Where(x => x.IsDeleted && !x.IsNew)));
                return artist.ArtistId.Value;
            }
            else
            {
                var newArtist = ArtistDtoToArtistMapper.Map(artist);
                artistService.Save(newArtist);
                artistService.SaveTags(newArtist, ArtistDtoToArtistMapper.MapTags(artist.ArtistTags), null, null);
                return newArtist.ArtistId.Value;
            }
        }

        [Route("{id:int}")]
        public void Delete(int id)
        {
            artistService.Delete(id);
        }
    }
}
