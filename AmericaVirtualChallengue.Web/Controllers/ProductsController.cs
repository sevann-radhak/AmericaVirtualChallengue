namespace AmericaVirtualChallengue.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AmericaVirtualChallengue.Web.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models.Data;
    using Models.Data.Entities;
    using Models.ModelsView;

    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: Products
        public IActionResult Index()
        {
            //TODO: hidden disabled products
            return View(this.productRepository.GetAll().OrderBy(p => p.Name));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                //TODO: Complete the redirections of project
                return new NotFoundViewResult("ProductNotFound");
            }

            Product product = await this.productRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            List<Topic> topics = this.productRepository.GetTopicsByProduct(product);

            ProductViewAPI pVApi = this.productRepository.ToProductViewAPI(product, topics);


            return View(pVApi);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel view)
        {
            if (ModelState.IsValid)
            {
                // Verify if user sent a photo
                string path = string.Empty;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    string guid = Guid.NewGuid().ToString();

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Products",
                        $"{guid}{view.ImageFile.FileName}");

                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Products/{guid}{view.ImageFile.FileName}";
                }

                // Create the Product object
                Product product = this.ToProduct(view, path);

                await this.productRepository.CreateAsync(product);
                //await this.repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await this.productRepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            //TODO: Include edit topics or remove it
            // Transform to ProductViewModel object
            ProductViewModel view = this.ToProductViewModel(product);
            //List<Topic> topics = this.productRepository.GetTopicsByProduct(product);

            //ProductViewAPI view = this.productRepository.ToProductViewAPI(product,topics);

            return View(view);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel view)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Verify if user sent a photo
                    string path = view.ImageUrl;

                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {
                        string guid = Guid.NewGuid().ToString();

                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\Products",
                            $"{guid}{view.ImageFile.FileName}");

                        using (FileStream stream = new FileStream(path, FileMode.Create))
                        {
                            await view.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/Products/{guid}{view.ImageFile.FileName}";
                    }

                    // Transform to Product object
                    Product product = this.ToProduct(view, path);
                    await this.productRepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.productRepository.ExistAsync(view.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await this.productRepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product product = await this.productRepository.GetByIdAsync(id);
            try
            {
                await this.productRepository.DeleteAsync(product);

            }
            catch (Exception)
            {
                ModelState.AddModelError("Error", "You can not delete this object because it has related records");
                return View(product);
            }

            return RedirectToAction(nameof(Index));
        }

        private Product ToProduct(ProductViewModel view, string path)
        {
            return new Product
            {
                Description = view.Description,
                Id = view.Id,
                ImageUrl = path,
                IsAvailabe = view.IsAvailabe,
                Name = view.Name,
                Price = view.Price
            };
        }

        private ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Description = product.Description,
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                IsAvailabe = product.IsAvailabe,
                Name = product.Name,
                Price = product.Price
            };
        }

        public IActionResult ProductNotFound()
        {
            return this.View();
        }

    }
}
