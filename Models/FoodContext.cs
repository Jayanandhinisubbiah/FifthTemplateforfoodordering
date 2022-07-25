using Microsoft.EntityFrameworkCore;

namespace FifthTemplateforfoodordering.Models
{
    public class FoodContext:DbContext
    {
        public FoodContext() { }
        public FoodContext(DbContextOptions<FoodContext> options) : base(options) { }
        public DbSet<Food> Foods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderMaster> OrderMasters { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Cart> Cart { get; set; }

    }
}
