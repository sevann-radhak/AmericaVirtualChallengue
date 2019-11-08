namespace AmericaVirtualChallengue.Web.Controllers.API
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Models.Data;
    using Models.Data.Entities;
    using Models.ModelsView;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// GET: Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(this.productRepository.GetAll().OrderByDescending(p => p.Id));
        }

        /// <summary>
        /// GET: Products/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

            return Ok(pVApi);
        }
    }
}
