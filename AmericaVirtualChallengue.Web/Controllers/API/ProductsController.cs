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
    using AmericaVirtualChallengue.Web.Helpers;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IUserHelper userHelper;

        public ProductsController(IProductRepository productRepository, IUserHelper userHelper)
        {
            this.productRepository = productRepository;
            this.userHelper = userHelper;
        }

        /// <summary>
        /// GET: Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
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
        [AllowAnonymous]
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

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            //TODO: Upload images
            var entityProduct = new Product
            {
                IsAvailabe = product.IsAvailabe,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl
            };

            var newProduct = await this.productRepository.CreateAsync(entityProduct);
            return Ok(newProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            var oldProduct = await this.productRepository.GetByIdAsync(id);
            if (oldProduct == null)
            {
                return this.BadRequest("Product Id does not exist.");
            }

            //TODO: Upload images
            oldProduct.IsAvailabe = product.IsAvailabe;
            oldProduct.Name = product.Name;
            oldProduct.Price = product.Price;
            oldProduct.ImageUrl = product.ImageUrl;
            oldProduct.Description = product.Description;

            var updatedProduct = await this.productRepository.UpdateAsync(oldProduct);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var product = await this.productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return this.NotFound();
            }

            await this.productRepository.DeleteAsync(product);
            return Ok(product);
        }

    }
}
