using System.Collections.Generic;
using Rema.Domain.Entity;

namespace Rema.Domain.Service.Abstract
{
    interface IRecordingService
    {
        List<Recording> ListBySite(string site);
    }
}
