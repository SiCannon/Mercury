using System.Collections.Generic;
using Rema.Domain.Entity;
using Rema.Domain.Service.Abstract;

namespace Rema.Domain.Service.Concrete
{
    public class CompanyService : ICompanyService
    {
        public List<Company> ListAll()
        {
            string query = "select COMPANY_CODE, COMPANY_NAME from COMPANY";

            return Helpers.Database.GetList(query, x => new Company(x));
        }
    }
}
