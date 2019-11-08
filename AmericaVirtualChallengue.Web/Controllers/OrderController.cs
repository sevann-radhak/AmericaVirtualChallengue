namespace AmericaVirtualChallengue.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Data;
    using Models.Data.Repositories;
    using Models.ModelsView;

    [Authorize(Roles = "Admin, User")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        private readonly Serilog.ILogger seriLogger;

        public OrdersController(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            Serilog.ILogger seriLogger)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.seriLogger = seriLogger;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            System.Linq.IQueryable<Models.Data.Entities.Order> model = await orderRepository.GetOrdersAsync(this.User.Identity.Name);
            return View(model);
        }

        /// <summary>
        /// Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            if (!await orderRepository.ExistAsync(id))
            {
                return this.RedirectToAction("/Home/Error404");
            }

            OrderViewModel model = await orderRepository.GetOrderDetailAsync(id, this.User.Identity.Name);

            if (model == null)
            {
                return this.RedirectToAction("NotAuthorized", "Account");
            }

            return View(model);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Create()
        {
            System.Linq.IQueryable<Models.Data.Entities.OrderDetailTemp> model = await this.orderRepository.GetDetailTempsAsync(this.User.Identity.Name);
            return this.View(model);
        }

        /// <summary>
        /// AddProduct
        /// </summary>
        /// <returns></returns>
        public IActionResult AddProduct()
        {
            AddItemViewModel model = new AddItemViewModel
            {
                Quantity = 1,
                Products = this.productRepository.GetComboProducts()
            };

            return View(model);
        }

        /// <summary>
        /// POST: AddProduct to temp order
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddItemViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await this.orderRepository.AddItemToOrderAsync(model, this.User.Identity.Name);
                return this.RedirectToAction("Create");
            }

            return this.View(model);
        }

        /// <summary>
        /// Delete item from temp order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.orderRepository.DeleteDetailTempAsync(id.Value);

            return this.RedirectToAction("Create");
        }

        /// <summary>
        /// Increase
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.orderRepository.ModifyOrderDetailTempQuantityAsync(id.Value, 1);
            return this.RedirectToAction("Create");
        }

        /// <summary>
        /// Decrease
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.orderRepository.ModifyOrderDetailTempQuantityAsync(id.Value, -1);
            return this.RedirectToAction("Create");
        }

        /// <summary>
        /// ConfirmOrder
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ConfirmOrder()
        {
            bool response = await this.orderRepository.ConfirmOrderAsync(this.User.Identity.Name);
            if (response)
            {
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Create");
        }

    }

}
