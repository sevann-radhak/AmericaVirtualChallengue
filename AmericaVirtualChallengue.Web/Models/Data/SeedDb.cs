namespace AmericaVirtualChallengue.Web.Models.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AmericaVirtualChallengue.Web.Helpers;
    using Entities;
    using Microsoft.AspNetCore.Identity;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            // Verify roles
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("User");

            //TODO: Verify what happens if there is not DB conection
            // Verify Users
            var user = await this.userHelper.FindByEmailAsync("sevann.radhak@gmail.com");

            // Verify Products, then create initial data
            if (user == null
                && !this.context.Products.Any()
                && !this.context.Options.Any()
                && !this.context.Topics.Any()
                && !this.context.Sales.Any())
            {
                // Create the user
                user = new User
                {
                    FirstName = "Sevann",
                    LastName = "Radhak",
                    Email = "sevann.radhak@gmail.com",
                    UserName = "sevann.radhak@gmail.com",
                    PhoneNumber = "5491173627795"
                };

                var result = await this.userHelper.CreateAsync(user, "sevann.radhak@gmail.com");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                // Add role to user
                //await this.userHelper.AddUserToRoleAsync(user, "Admin");
                var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
                if (!isInRole)
                {
                    await this.userHelper.AddUserToRoleAsync(user, "Admin");
                }

                // Add options
                Option bronze = new Option { Description = "Bronze" };
                Option plate = new Option { Description = "Plate" };
                Option golden = new Option { Description = "Golden" };
                this.AddOption(bronze);
                this.AddOption(plate);
                this.AddOption(golden);

                // Add topics
                Topic firstTopic = new Topic { Description = "Requirements analysis" };
                Topic secondTopic = new Topic { Description = "Desktop and mobile interfaces design" };
                Topic thirdtTopic = new Topic { Description = "Frontend and backend development" };
                this.AddTopic(firstTopic);
                this.AddTopic(secondTopic);
                this.AddTopic(thirdtTopic);

                // Add products
                Product firstProduct = new Product
                {
                    Name = "Serv Dev: Webapp ecommerce",
                    Price = this.random.Next(100),
                    IsAvailabe = true
                };
                Product secondProduct = new Product
                {
                    Name = "Institutional Site",
                    Price = this.random.Next(100),
                    IsAvailabe = true
                };
                Product thirdProduct = new Product
                {
                    Name = "Game X",
                    Price = this.random.Next(100),
                    IsAvailabe = true
                };

                this.AddProduct(firstProduct);
                this.AddProduct(secondProduct);
                this.AddProduct(thirdProduct);

                // Add ProductsTopics
                this.AddProductsTopics(firstProduct, firstTopic);
                this.AddProductsTopics(firstProduct, secondTopic);
                this.AddProductsTopics(firstProduct, thirdtTopic);
                this.AddProductsTopics(secondProduct, firstTopic);
                this.AddProductsTopics(secondProduct, secondTopic);
                this.AddProductsTopics(thirdProduct, firstTopic);

                // Add a sale
                Sale sale = new Sale
                {
                    Date = DateTime.Now,
                    Option = bronze,
                    Price = this.random.Next(100),
                    User = user
                };
                this.AddSale(sale);

                // Add the sale details
                this.AddSaleDetails(new SalesDetail
                {
                    Product = firstProduct,
                    Quantity = 2,
                    Price = firstProduct.Price * 2,
                    Sale = sale
                });

                var resultSeed = await this.context.SaveChangesAsync() > 0;

                if (!resultSeed)
                {
                    throw new InvalidOperationException("Could not create the initial data in seeder");
                }
            }
        }

        private void AddOption(Option option)
        {
            this.context.Options.Add(option);
        }

        private void AddTopic(Topic topic)
        {
            this.context.Topics.Add(topic);
        }

        private void AddProduct(Product product)
        {
            this.context.Products.Add(product);
        }

        private void AddProductsTopics(Product product, Topic topic)
        {
            this.context.ProductTopics.Add(new ProductsTopic
            {
                Product = product,
                Topic = topic
            });
        }

        private void AddSale(Sale sale)
        {
            this.context.Sales.Add(sale);
        }

        private void AddSaleDetails(SalesDetail salesDetail)
        {
            this.context.SalesDetails.Add(salesDetail);
        }
    }

}
