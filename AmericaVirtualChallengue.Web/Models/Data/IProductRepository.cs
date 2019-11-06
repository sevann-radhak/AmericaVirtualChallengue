namespace AmericaVirtualChallengue.Web.Models.Data
{
    using ModelsView;
    using Entities;
    using System.Linq;
    using System.Collections.Generic;

    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Topic> GetTopicsByProduct(Product product);
        ProductViewAPI ToProductViewAPI(Product product, List<Topic> topics);
        //List<Topic> GetTopicsByProduct(Product product);
    }

}
