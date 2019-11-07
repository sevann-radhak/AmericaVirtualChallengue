﻿namespace AmericaVirtualChallengue.Web.Models.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using AmericaVirtualChallengue.Web.Models.ModelsView;
    using Entities;

    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IQueryable<Order>> GetOrdersAsync(string userName);

        Task<IQueryable<OrderDetailTemp>> GetDetailTempsAsync(string userName);

        Task AddItemToOrderAsync(AddItemViewModel model, string userName);

        Task ModifyOrderDetailTempQuantityAsync(int id, double quantity);

        Task DeleteDetailTempAsync(int id);

        Task<bool> ConfirmOrderAsync(string userName);
    }
}
