namespace AmericaVirtualChallengue.Web.Controllers.API
{
    using AmericaVirtualChallengue.Web.Models.Data;
    using AmericaVirtualChallengue.Web.Models.Data.Entities;
    using AmericaVirtualChallengue.Web.Models.ModelsView;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
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

            Product product = await this.productRepository.GetByIdAsync(id);
            List<Topic> topics = this.productRepository.GetTopicsByProduct(product);

            ProductViewAPI pVApi = this.productRepository.ToProductViewAPI(product, topics);

            //return Ok(this.productRepository.GetProduct(id));
            return Ok(pVApi);
        }
    }
}
