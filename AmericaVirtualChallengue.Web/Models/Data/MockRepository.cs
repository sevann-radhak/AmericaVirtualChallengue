using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmericaVirtualChallengue.Web.Models.Data.Entities;

namespace AmericaVirtualChallengue.Web.Models.Data
{
    public class MockRepository : IRepository
    {
        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();

            products.Add(new Product { Id = 1, Name = "First product", Price = 10 });
            products.Add(new Product { Id = 1, Name = "Second product", Price = 15 });
            products.Add(new Product { Id = 1, Name = "Third product", Price = 20 });

            return products;
        }

        public bool ProductExists(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
