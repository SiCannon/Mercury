using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hub.Website
{
    public class DatabaseConfig
    {
        public static void Init()
        {
            Hub.Domain.Infrastructure.HubStartup.Go();
            Hub.Domain.Convert.Products.LoadProductsFromXml();
        }
    }
}