using System;
using System.Data;

namespace Rema.Domain.Entity
{
    public class Label
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public Label()
        {

        }

        public Label(IDataRecord row)
        {
            Code = Helpers.Database.GetString(row, "LABEL_CODE");
            Name = Helpers.Database.GetString(row, "LABEL_DESC");
        }
    }
}
