namespace AmericaVirtualChallengue.Web.Models.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using ModelsView;

    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext context;

        public ProductRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        /// <summary>
        /// GetTopicsByProduct
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public List<Topic> GetTopicsByProduct(Product product)
        {
            List<ProductsTopic> pts = context.ProductTopics.Where(pt => pt.Product == product).Include(pt => pt.Topic).ToList();
            List<Topic> topics = new List<Topic>();

            foreach (ProductsTopic pt in pts)
            {
                topics.Add(pt.Topic);
            }

            return topics;
        }

        /// <summary>
        /// ToProductViewAPI
        /// </summary>
        /// <param name="product"></param>
        /// <param name="topics"></param>
        /// <returns></returns>
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

        /// <summary>
        /// GetComboProducts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetComboProducts()
        {
            List<SelectListItem> list = this.context.Products.Select(p => new SelectListItem
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
