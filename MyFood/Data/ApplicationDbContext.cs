namespace MyFood.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }
        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Food> Foods { get; set; }

        public DbSet<FoodOrder> FoodOrders { get; set; }

        public DbSet<Location> Locations { get; set; }
        
        public DbSet<Order> Orders { get; set; }


    }
}
