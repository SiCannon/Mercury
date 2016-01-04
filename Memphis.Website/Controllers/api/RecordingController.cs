using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Memphis.BusinessLogic.Interface;
using Memphis.Website.Dto;

namespace Memphis.Website.Controllers.api
{
    [RoutePrefix("api/Recording")]
    public class RecordingController : ApiController
    {
        IMappingEngine mapper;
        IRecordingService recordingService;

        public RecordingController(IMappingEngine mapper, IRecordingService recordingService)
        {
            this.mapper = mapper;
            this.recordingService = recordingService;
        }

        [Route("")]
        public IEnumerable<RecordingSelectItemDto> Get(int pageNumber = 0, int pageSize = 20, string searchField = "title", string searchText = "", string sortBy = "title", bool sortAscending = true)
        {
            return mapper.Map<List<RecordingSelectItemDto>>(recordingService.Query(pageNumber, pageSize, searchField, searchText, sortBy, sortAscending));
        }

        [Route("{id:int}")]
        public RecordingSelectItemDto Get(int id)
        {
            return mapper.Map<RecordingSelectItemDto>(recordingService.GetById(id));
        }
    }
}
