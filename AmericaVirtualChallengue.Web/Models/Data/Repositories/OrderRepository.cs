namespace AmericaVirtualChallengue.Web.Models.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Helpers;
    using Microsoft.EntityFrameworkCore;
    using ModelsView;

    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private readonly Serilog.ILogger seriLogger;

        public OrderRepository(
            DataContext context,
            IUserHelper userHelper,
            Serilog.ILogger seriLogger)
            : base(context)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.seriLogger = seriLogger;
        }

        /// <summary>
        /// GetOrdersAsync
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IQueryable<Order>> GetOrdersAsync(string userName)
        {
            User user = await this.userHelper.FindByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await this.userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return this.context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .OrderByDescending(o => o.OrderDate);
            }

            return this.context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.User == user)
                .OrderByDescending(o => o.OrderDate);
        }

        /// <summary>
        /// GetOrderDetailAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<OrderViewModel> GetOrderDetailAsync(int id, string userName)
        {
            OrderViewModel view = null;

            User user = await this.userHelper.FindByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (!await this.ExistAsync(id))
            {
                return null;
            }

            if (await this.userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                Order order = this.context.Orders
                    .Include(u => u.User)
                    .Include(i => i.Items)
                    .ThenInclude(p => p.Product)
                    .Where(od => od.Id == id)
                    .FirstOrDefault();

                view = new OrderViewModel
                {
                    Id = order.Id,
                    Items = order.Items,
                    OrderDate = order.OrderDate,
                    User = order.User
                };
            }
            else
            {
                Order order = this.context.Orders
                    .Include(u => u.User)
                    .Include(i => i.Items)
                    .ThenInclude(p => p.Product)
                    .Where(od => od.Id == id)
                    .FirstOrDefault();

                if (order.User != user)
                {
                    return null;
                }

                view = new OrderViewModel
                {
                    Id = order.Id,
                    Items = order.Items,
                    OrderDate = order.OrderDate,
                    User = order.User
                };
            }

            return view;
        }

        /// <summary>
        /// GetDetailTempsAsync
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<IQueryable<OrderDetailTemp>> GetDetailTempsAsync(string userName)
        {
            User user = await this.userHelper.FindByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            return this.context.OrderDetailTemps
                .Include(o => o.Product)
                .Where(o => o.User == user)
                .OrderBy(o => o.Product.Name);
        }

        /// <summary>
        /// AddItemToOrderAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task AddItemToOrderAsync(AddItemViewModel model, string userName)
        {
            User user = await this.userHelper.FindByEmailAsync(userName);
            if (user == null)
            {
                return;
            }

            Product product = await this.context.Products.FindAsync(model.ProductId);
            if (product == null)
            {
                return;
            }

            OrderDetailTemp orderDetailTemp = await this.context.OrderDetailTemps
                .Where(odt => odt.User == user && odt.Product == product)
                .FirstOrDefaultAsync();
            if (orderDetailTemp == null)
            {
                orderDetailTemp = new OrderDetailTemp
                {
                    Price = product.Price,
                    Product = product,
                    Quantity = model.Quantity,
                    User = user,
                };

                this.context.OrderDetailTemps.Add(orderDetailTemp);
            }
            else
            {
                orderDetailTemp.Quantity += model.Quantity;
                this.context.OrderDetailTemps.Update(orderDetailTemp);
            }

            await this.context.SaveChangesAsync();

            // LOG: DateTime now, DateTime now London, userName, action, product description, product quantity, product price 
            string logMessage = $"{DateTime.Now} | {DateTime.UtcNow} | {userName} | Add Item to newOrder | {product.Description} |  {model.Quantity} | {product.Price}";
            seriLogger.Warning(logMessage);
        }

        /// <summary>
        /// ModifyOrderDetailTempQuantityAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public async Task ModifyOrderDetailTempQuantityAsync(int id, double quantity)
        {
            OrderDetailTemp orderDetailTemp = await this.context.OrderDetailTemps.FindAsync(id);
            if (orderDetailTemp == null)
            {
                return;
            }

            orderDetailTemp.Quantity += quantity;
            if (orderDetailTemp.Quantity > 0)
            {
                this.context.OrderDetailTemps.Update(orderDetailTemp);
                await this.context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// DeleteDetailTempAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteDetailTempAsync(int id)
        {
            OrderDetailTemp orderDetailTemp = await this.context.OrderDetailTemps.FindAsync(id);
            if (orderDetailTemp == null)
            {
                return;
            }

            this.context.OrderDetailTemps.Remove(orderDetailTemp);
            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Confirm the order after adding items
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool> ConfirmOrderAsync(string userName)
        {
            User user = await this.userHelper.FindByEmailAsync(userName);
            if (user == null)
            {
                return false;
            }

            System.Collections.Generic.List<OrderDetailTemp> orderTmps = await this.context.OrderDetailTemps
                .Include(o => o.Product)
                .Where(o => o.User == user)
                .ToListAsync();

            if (orderTmps == null || orderTmps.Count == 0)
            {
                return false;
            }

            System.Collections.Generic.List<OrderDetail> details = orderTmps.Select(o => new OrderDetail
            {
                Price = o.Price,
                Product = o.Product,
                Quantity = o.Quantity
            }).ToList();

            Order order = new Order
            {
                OrderDate = DateTime.UtcNow,
                User = user,
                Items = details,
            };

            this.context.Orders.Add(order);
            this.context.OrderDetailTemps.RemoveRange(orderTmps);
            await this.context.SaveChangesAsync();

            // LOG: DateTime now, DateTime now London, userName, action, orderId, items
            string logMessage = $"{DateTime.Now} | {DateTime.UtcNow} | {userName} | Confirm Order | {order.Id}";
            seriLogger.Warning(logMessage);

            return true;
        }
    }

}
