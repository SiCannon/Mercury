using System;
using System.Data;

namespace Rema.Domain.Entity
{
    public class Company
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public Company()
        {

        }

        public Company(IDataRecord row)
        {
            Code = Helpers.Database.GetString(row, "COMPANY_CODE");
            Name = Helpers.Database.GetString(row, "COMPANY_NAME");
        }
    }
}
