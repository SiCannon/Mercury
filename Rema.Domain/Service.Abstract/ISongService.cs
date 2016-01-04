using System.Collections.Generic;
using Rema.Domain.Entity;

namespace Rema.Domain.Service.Abstract
{
    interface ISongService
    {
        Song GetBySiteAndCode(string site, int code);
        List<Song> ListBySite(string site, bool mustHaveIswc, int? maxResults);
    }
}
