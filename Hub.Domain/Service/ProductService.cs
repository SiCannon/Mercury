using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Hub.Domain.Abstract;
using Hub.Domain.Entity;
using Hub.Domain.Infrastructure;

namespace Hub.Domain.Service
{
    public class ProductService : IProductService
    {
        HubContext context = new HubContext();

        public IEnumerable<Product> ListAll()
        {
            return context.Products;
        }

        public Product GetById(int? id)
        {
            return context.Products.SingleOrDefault(p => p.ProductId == id);
        }

        public void Save(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                context.Products.Attach(product);
                context.Entry(product).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            var p = GetById(product.ProductId);
            if (p != null)
            {
                context.Products.Remove(p);
                context.SaveChanges();
            }
        }
    }
}
