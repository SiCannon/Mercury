using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Memphis.BusinessLogic.Dto;
using Memphis.BusinessLogic.Interface;
using Memphis.Website.Dto;

namespace Memphis.Website.Controllers.api
{
    [RoutePrefix("api/Album")]
    public class AlbumController : ApiController
    {
        IMappingEngine mapper;
        IAlbumService albumService;

        public AlbumController(IMappingEngine mapper, IAlbumService albumService)
        {
            this.mapper = mapper;
            this.albumService = albumService;
        }

        [Route("")]
        public IEnumerable<AlbumListItemDto> Get(int pageNumber = 0, int pageSize = 20, string searchField = "title", string searchText = "", string sortBy = "title", bool sortAscending = true)
        {
            return mapper.Map<List<AlbumListItemDto>>(albumService.Query(pageNumber, pageSize, searchField, searchText, sortBy, sortAscending));
        }

        [Route("Count")]
        public int GetCount(string searchField = "title", string searchText = "")
        {
            return albumService.Count(searchField, searchText);
        }

        [Route("{id:int}")]
        public AlbumDetailDto_OLD Get(int id)
        {
            return mapper.Map<AlbumDetailDto_OLD>(albumService.GetById(id));
        }

        [Route("")]
        public int Post([FromBody]AlbumSaveDto album)
        {
            albumService.Save(album);
            return album.AlbumId.Value;
        }

    }
}
