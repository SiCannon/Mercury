using System;
using Memphis.BusinessLogic.Interface;

namespace Mercury.Console.Generate
{
    class ConvertTop3kToMemphis
    {
        IArtistService artistService;
        ITagService tagService;
        IAlbumService releaseService;

        public ConvertTop3kToMemphis(IArtistService artistService, ITagService tagService, IAlbumService releaseService)
        {
            this.artistService = artistService;
            this.tagService = tagService;
            this.releaseService = releaseService;
        }

        public void Go()
        {
            //TODO
            //This will replace ArtistsForMemphis
        }
    }
}
