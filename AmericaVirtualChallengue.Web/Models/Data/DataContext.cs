namespace AmericaVirtualChallengue.Web.Models.Data
{
    using AmericaVirtualChallengue.Web.Models.Data.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<ProductsTopic> ProductTopics { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesDetail> SalesDetails { get; set; }
        public DbSet<Topic> Topics { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

    }
}
