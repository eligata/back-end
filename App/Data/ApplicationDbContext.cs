using App.Common.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace APP.API.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Basket> Baskets { get; set; }

        public DbSet<ProductInBasket> ProductInBaskets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ///
            /// As lazy loading isn't supported in current EF version
            /// this is done to keep retreiveing data clean and simple
            foreach (var item in Baskets)
                item.ProductsInBasket = ProductInBaskets.Where(x => x.BasketId == item.Id).ToList();

            foreach (var item in ProductInBaskets)
                item.Product = Products.SingleOrDefault(x => x.Id == item.ProductId);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
