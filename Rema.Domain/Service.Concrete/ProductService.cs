using System.Collections.Generic;
using Rema.Domain.Entity;
using Rema.Domain.Service.Abstract;

namespace Rema.Domain.Service.Concrete
{
    public class ProductService : IProductService
    {
        string productFields = "PRODUCT_SITE, PRODUCT_CODE, PRODUCT_USERCODE, PRODUCT_BARCODE, PRODUCT_TITLE, PRODUCT_ARTIST, COMPANY_CODE, LABEL_CODE, CONFIG_CODE, PRODUCT_ACTUALRELDATE";

        public List<Product> ListBySite(string site, int? maxResults = null)
        {
            string rowNumFilter = maxResults.HasValue ? string.Format("and rownum <= {0}", maxResults) : "";
            string query = string.Format("select {0} from PRODUCTS where PRODUCT_SITE = '{1}' {2}", productFields, site, rowNumFilter);

            return Helpers.Database.GetList(query, x => new Product(x));
        }

        public List<Product> ListAll()
        {
            return Helpers.Database.GetList(string.Format("select {0} from PRODUCTS", productFields), x => new Product(x));
        }
    }
}
