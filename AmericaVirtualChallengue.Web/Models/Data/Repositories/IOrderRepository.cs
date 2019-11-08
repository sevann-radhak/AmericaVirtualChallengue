namespace AmericaVirtualChallengue.Web.Models.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using ModelsView;

    public interface IOrderRepository : IGenericRepository<Order>
    {
        /// <summary>
        /// GetOrdersAsync
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<IQueryable<Order>> GetOrdersAsync(string userName);

        /// <summary>
        /// GetOrderDetailAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<OrderViewModel> GetOrderDetailAsync(int id, string userName);

        /// <summary>
        /// GetDetailTempsAsync
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<IQueryable<OrderDetailTemp>> GetDetailTempsAsync(string userName);

        /// <summary>
        /// AddItemToOrderAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task AddItemToOrderAsync(AddItemViewModel model, string userName);

        /// <summary>
        /// ModifyOrderDetailTempQuantityAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);

        /// <summary>
        /// DeleteDetailTempAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteDetailTempAsync(int id);

        /// <summary>
        /// ConfirmOrderAsync
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> ConfirmOrderAsync(string userName);
    }
}
