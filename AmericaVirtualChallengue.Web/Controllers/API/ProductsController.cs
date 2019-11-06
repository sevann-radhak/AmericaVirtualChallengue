namespace AmericaVirtualChallengue.Web.Controllers.API
{
    using AmericaVirtualChallengue.Web.Models.Data;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(this.productRepository.GetAll().OrderByDescending(p => p.Id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts(int id)
        {
            if (!await this.productRepository.ExistAsync(id))
            {
                return NotFound();
            }

            return Ok(this.productRepository.GetProductWithTopics(id));
        }
    }
}
