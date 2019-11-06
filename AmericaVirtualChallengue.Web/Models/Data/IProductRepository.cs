namespace AmericaVirtualChallengue.Web.Models.Data
{
    using ModelsView;
    using Entities;
    using System.Linq;

    public interface IProductRepository : IGenericRepository<Product>
    {
        ProductViewAPI GetProductWithTopics(int id);
    }

}
