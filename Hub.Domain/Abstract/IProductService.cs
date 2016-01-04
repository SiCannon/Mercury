using System;
using System.Collections.Generic;
using Hub.Domain.Entity;

namespace Hub.Domain.Abstract
{
    public interface IProductService
    {
        IEnumerable<Product> ListAll();
        Product GetById(int? id);
        void Save(Product product);
        void Delete(Product product);
    }
}
