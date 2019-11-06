namespace AmericaVirtualChallengue.Web.Models.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AmericaVirtualChallengue.Web.Models.ModelsView;
    using Entities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public List<Topic> GetTopicsByProduct(Product product)
        {
            List<ProductsTopic> pts = context.ProductTopics.Where(pt => pt.Product == product).Include(pt => pt.Topic).ToList();
            List<Topic> topics = new List<Topic>();

            foreach (var pt in pts)
            {
                topics.Add(pt.Topic);
            }

            return topics;
        }

        public ProductViewAPI ToProductViewAPI(Product product, List<Topic> topics)
        {
            ProductViewAPI pVApi = new ProductViewAPI
            {
                Description = product.Description,
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                IsAvailabe = product.IsAvailabe,
                Name = product.Name,
                Price = product.Price,
                Topics = topics
            };

            return pVApi;
        }

        public IEnumerable<SelectListItem> GetComboProducts()
        {
            var list = this.context.Products.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a product...)",
                Value = "0"
            });

            return list;
        }
    }
}
