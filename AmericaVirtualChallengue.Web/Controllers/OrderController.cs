namespace AmericaVirtualChallengue.Web.Controllers
{
    using Models.Data;
    using Models.ModelsView;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Data.Repositories;
    using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            System.Linq.IQueryable<Models.Data.Entities.Order> model = await orderRepository.GetOrdersAsync(this.User.Identity.Name);
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            if (!await orderRepository.ExistAsync(id)) return this.RedirectToAction("/Home/Error404");

            var model = await orderRepository.GetOrderDetailAsync(id, this.User.Identity.Name);

            if (model == null) return this.RedirectToAction("NotAuthorized", "Account");

            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            System.Linq.IQueryable<Models.Data.Entities.OrderDetailTemp> model = await this.orderRepository.GetDetailTempsAsync(this.User.Identity.Name);
            return this.View(model);
        }

        public IActionResult AddProduct()
        {
            AddItemViewModel model = new AddItemViewModel
            {
                Quantity = 1,
                Products = this.productRepository.GetComboProducts()
            };

            return View(model);
        }

        // Add a product to order temp
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

        // Delete item from temp order
        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.orderRepository.DeleteDetailTempAsync(id.Value);

            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.orderRepository.ModifyOrderDetailTempQuantityAsync(id.Value, 1);
            return this.RedirectToAction("Create");
        }

        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await this.orderRepository.ModifyOrderDetailTempQuantityAsync(id.Value, -1);
            return this.RedirectToAction("Create");
        }

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
