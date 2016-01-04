using System.Collections.Generic;
using Rema.Domain.Entity;
using Rema.Domain.Service.Abstract;

namespace Rema.Domain.Service.Concrete
{
    public class TrackService : ITrackService
    {
        public List<Track> ListByProductSite(string site)
        {
            return Helpers.Database.GetList(string.Format("select * from PRODTRACK where PRODUCT_SITE = '{0}'", site), x => new Track(x));
        }
    }
}
