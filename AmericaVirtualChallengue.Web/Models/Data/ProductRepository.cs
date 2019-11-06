namespace AmericaVirtualChallengue.Web.Models.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using AmericaVirtualChallengue.Web.Models.ModelsView;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public ProductViewAPI GetProductWithTopics(int id)
        {
            Product product = context.Products.Find(id);

            if(product == null)
            {
                return null;
            }

            List<ProductsTopic> pts = context.ProductTopics.Where(pt => pt.Product == product).Include(pt =>  pt.Topic).ToList();
            List<Topic> topics = new List<Topic>();

            foreach (var pt in pts)
            {
                topics.Add(pt.Topic);
            }

            ProductViewAPI pApi = new ProductViewAPI
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                IsAvailabe = product.IsAvailabe,
                Name = product.Name,
                Price = product.Price,
                Topics = topics
            };

            return pApi;
        }
    }

}
