using System.Collections.Generic;
using Rema.Domain.Entity;

namespace Rema.Domain.Service.Abstract
{
    interface IProductService
    {
        List<Product> ListBySite(string site, int? maxResults = null);
        List<Product> ListAll();
        //List<Product> ListBySite
    }
}
