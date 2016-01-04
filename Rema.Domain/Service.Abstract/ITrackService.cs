using System.Collections.Generic;
using Rema.Domain.Entity;

namespace Rema.Domain.Service.Abstract
{
    interface ITrackService
    {
        List<Track> ListByProductSite(string site);
    }
}
