using System.Collections.Generic;
using Rema.Domain.Entity;

namespace Rema.Domain.Service.Abstract
{
    interface IConfigService
    {
        List<Config> ListAll();
    }
}
