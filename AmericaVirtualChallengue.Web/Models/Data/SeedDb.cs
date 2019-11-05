namespace AmericaVirtualChallengue.Web.Models.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public class SeedDb
    {
        private readonly DataContext context;
        private Random random;

        public SeedDb(DataContext context)
        {
            this.context = context;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            //TODO: Verify what happens if there is not DB conection

            // Verify Categories
            if (!this.context.Options.Any())
            {
                this.Option("Bronze");
                this.Option("Plate");
                this.Option("Golden");

                await this.context.SaveChangesAsync();
            }

            // Verify Products
            if (!this.context.Products.Any())
            {
                this.AddProduct("Serv Dev: Webapp ecommerce");
                this.AddProduct("Institutional Site");
                this.AddProduct("Game X");

                await this.context.SaveChangesAsync();
            }
        }

        private void Option(string description)
        {
            this.context.Options.Add(new Option{
                Description = description
            });
        }

        private void AddProduct(string name)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(100),
                IsAvailabe = true
            });
        }
    }

}
