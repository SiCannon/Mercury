using System.Collections.Generic;
using Rema.Domain.Entity;
using Rema.Domain.Service.Abstract;

namespace Rema.Domain.Service.Concrete
{
    public class ConfigService : IConfigService
    {
        public List<Config> ListAll()
        {
            return Helpers.Database.GetList("select CONFIG_CODE, CONFIG_DESC from CONFIG", x => new Config(x));
        }
    }
}
