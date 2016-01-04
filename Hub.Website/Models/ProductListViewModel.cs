using System.Collections.Generic;
using Hub.Domain.Entity;

namespace Hub.Website.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
    }
}