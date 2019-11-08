namespace AmericaVirtualChallengue.Web.Models.Data
{
    using System.Collections.Generic;
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using ModelsView;

    public interface IProductRepository : IGenericRepository<Product>
    {
        /// <summary>
        /// GetTopicsByProduct
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        List<Topic> GetTopicsByProduct(Product product);

        /// <summary>
        /// ToProductViewAPI
        /// </summary>
        /// <param name="product"></param>
        /// <param name="topics"></param>
        /// <returns></returns>
        ProductViewAPI ToProductViewAPI(Product product, List<Topic> topics);

        /// <summary>
        /// GetComboProducts
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetComboProducts();
    }
}
