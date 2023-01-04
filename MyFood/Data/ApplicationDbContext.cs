namespace MyFood.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FoodOrder>()
                .HasOne(m => m.User)
                .WithMany(m => m.FoodItemOrders)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<ApplicationUser> Users { get; set; }

        public DbSet<Food> Foods { get; set; }

        public DbSet<FoodOrder> FoodOrders { get; set; }

        public DbSet<Location> Locations { get; set; }
        
        public DbSet<Order> Orders { get; set; }


    }
}
