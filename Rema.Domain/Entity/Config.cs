using System;
using System.Data;

namespace Rema.Domain.Entity
{
    public class Config
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public Config()
        {

        }

        public Config(IDataRecord row)
        {
            Code = Helpers.Database.GetString(row, "CONFIG_CODE");
            Name = Helpers.Database.GetString(row, "CONFIG_DESC");
        }
    }
}
