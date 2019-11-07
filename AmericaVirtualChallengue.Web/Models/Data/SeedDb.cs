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

            // Check roles
            await this.CheckRoles();

            await this.CheckUserAsync("virtual.america@gmail.com", "Virtual", "America", "User");
            await this.CheckUserAsync("homero.simpson@gmail.com", "Homero", "Simpson", "User");
            await this.CheckUserAsync("sevann.radhak@gmail.com", "Sevann", "Radhak", "Admin");

            // Add products
            if (!this.context.Products.Any())
            {
                // Add topics
                Topic firstTopic = new Topic { Description = "Requirements analysis" };
                Topic secondTopic = new Topic { Description = "Desktop and mobile interfaces design" };
                Topic thirdtTopic = new Topic { Description = "Frontend and backend development" };
                this.AddTopic(firstTopic);
                this.AddTopic(secondTopic);
                this.AddTopic(thirdtTopic);

                Product product = this.AddProduct(
                    "Serv Dev: Webapp ecommerce",
                    "Aplicación web para servicios relacionados con ecommerce. Especial para sus necesidades",
                    "ecommerce",
                    15900);
                this.AddProductsTopics(product, firstTopic);
                this.AddProductsTopics(product, secondTopic);
                this.AddProductsTopics(product, thirdtTopic);
                product = this.AddProduct(
                    "Institutional Site",
                    "Sitios institucionales para diferentes compañías. Pueden incluir determinadas reglas de negocio y amplio detalle en funcionalidades",
                    "institutional",
                    45900);
                this.AddProductsTopics(product, firstTopic);
                this.AddProductsTopics(product, thirdtTopic);
                this.AddProduct(
                    "Game Xs",
                    "Juego de aventura para aficionados, disfrute de su primera verión",
                    "gamexs",
                    4500);
                this.AddProductsTopics(product, secondTopic);

                for (int i = 0; i < 20; i++)
                {
                    product = this.AddProduct(
                    "Serv Dev: Webapp ecommerce Copy",
                    "Aplicación web evolucionada para servicios relacionados con ecommerce, incluye mayor soporte y nuevas herramientas de apoyo",
                    "ecommerce",
                    5000);
                    this.AddProductsTopics(product, firstTopic);
                    this.AddProductsTopics(product, secondTopic);
                    this.AddProductsTopics(product, thirdtTopic);
                }

                await this.context.SaveChangesAsync();
            }





            //TODO: Verify what happens if there is not DB conection
            // Verify Users
            //var user = await this.userHelper.FindByEmailAsync("sevann.radhak@gmail.com");

            //// Verify Products, then create initial data
            //if (user == null
            //    && !this.context.Products.Any()
            //    && !this.context.Options.Any()
            //    && !this.context.Topics.Any()
            //    && !this.context.Orders.Any())
            //{
            //    // Create the user
            //    //user = new User
            //    //{
            //    //    FirstName = "Sevann",
            //    //    LastName = "Radhak",
            //    //    Email = "sevann.radhak@gmail.com",
            //    //    UserName = "sevann.radhak@gmail.com",
            //    //    PhoneNumber = "5491173627795"
            //    //};

            //    //var result = await this.userHelper.CreateAsync(user, "sevann.radhak@gmail.com");

            //    //if (result != IdentityResult.Success)
            //    //{
            //    //    throw new InvalidOperationException("Could not create the user in seeder");
            //    //}

            //    // Add role to user
            //    //await this.userHelper.AddUserToRoleAsync(user, "Admin");
            //    //var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            //    //if (!isInRole)
            //    //{
            //    //    await this.userHelper.AddUserToRoleAsync(user, "Admin");
            //    //}

            //    //// Add options
            //    //Option bronze = new Option { Description = "Bronze" };
            //    //Option plate = new Option { Description = "Plate" };
            //    //Option golden = new Option { Description = "Golden" };
            //    //this.AddOption(bronze);
            //    //this.AddOption(plate);
            //    //this.AddOption(golden);

            //    //// Add topics
            //    //Topic firstTopic = new Topic { Description = "Requirements analysis" };
            //    //Topic secondTopic = new Topic { Description = "Desktop and mobile interfaces design" };
            //    //Topic thirdtTopic = new Topic { Description = "Frontend and backend development" };
            //    //this.AddTopic(firstTopic);
            //    //this.AddTopic(secondTopic);
            //    //this.AddTopic(thirdtTopic);

            //    // Add products
            //    Product firstProduct = new Product
            //    {
            //        Name = "Serv Dev: Webapp ecommerce",
            //        Price = this.random.Next(100),
            //        IsAvailabe = true
            //    };
            //    Product secondProduct = new Product
            //    {
            //        Name = "Institutional Site",
            //        Price = this.random.Next(100),
            //        IsAvailabe = true
            //    };
            //    Product thirdProduct = new Product
            //    {
            //        Name = "Game X",
            //        Price = this.random.Next(100),
            //        IsAvailabe = true
            //    };

            //    this.AddProduct(firstProduct);
            //    this.AddProduct(secondProduct);
            //    this.AddProduct(thirdProduct);

            //    // Add ProductsTopics
            //    this.AddProductsTopics(firstProduct, firstTopic);
            //    this.AddProductsTopics(firstProduct, secondTopic);
            //    this.AddProductsTopics(firstProduct, thirdtTopic);
            //    this.AddProductsTopics(secondProduct, firstTopic);
            //    this.AddProductsTopics(secondProduct, secondTopic);
            //    this.AddProductsTopics(thirdProduct, firstTopic);

            //    var resultSeed = await this.context.SaveChangesAsync() > 0;

            //    if (!resultSeed)
            //    {
            //        throw new InvalidOperationException("Could not create the initial data in seeder");
            //    }
            //}
        }

        private async Task CheckRoles()
        {
            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("User");
        }

        private async Task<User> CheckUserAsync(string userName, string firstName, string lastName, string role)
        {
            // Add user
            var user = await this.userHelper.FindByEmailAsync(userName);
            if (user == null)
            {
                user = await this.AddUser(userName, firstName, lastName, role);
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, role);
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }

        private async Task<User> AddUser(string userName, string firstName, string lastName, string role)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = userName,
                UserName = userName,
                PhoneNumber = "1173627795"
            };

            var result = await this.userHelper.CreateAsync(user, userName);
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create the user in seeder");
            }

            await this.userHelper.AddUserToRoleAsync(user, role);

            if(user.UserName == "sevann.radhak@gmail.com")
            {
                await this.userHelper.AddUserToRoleAsync(user, "User");
            }
            //var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
            //await this.userHelper.ConfirmEmailAsync(user, token);
            return user;
        }

        private Product AddProduct(string name, string description, string imageUrl, decimal price)
        {
            var product = new Product
            {
                Name = name,
                Price = price,
                Description = description,
                IsAvailabe = true,
                ImageUrl = $"~/images/Products/{imageUrl}.png"
            };

            this.context.Products.Add(product);

            return product;
        }

        private void AddProductsTopics(Product product, Topic topic)
        {
            this.context.ProductTopics.Add(new ProductsTopic
            {
                Product = product,
                Topic = topic
            });
        }


        private void AddTopic(Topic topic)
        {
            this.context.Topics.Add(topic);
        }




        //private void AddOption(Option option)
        //    {
        //        this.context.Options.Add(option);
        //    }

        //    private void AddTopic(Topic topic)
        //    {
        //        this.context.Topics.Add(topic);
        //    }

        //    private void AddProduct(Product product)
        //    {
        //        this.context.Products.Add(product);
        //    }

        //    private void AddProductsTopics(Product product, Topic topic)
        //    {
        //        this.context.ProductTopics.Add(new ProductsTopic
        //        {
        //            Product = product,
        //            Topic = topic
        //        });
        //    }

        //private void AddSale(Order sale)
        //{
        //    this.context.Sales.Add(sale);
        //}

        //private void AddSaleDetails(OrderDetail salesDetail)
        //{
        //    this.context.SalesDetails.Add(salesDetail);
        //}
    }

}
