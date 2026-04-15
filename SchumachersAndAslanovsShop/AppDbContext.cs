using Microsoft.EntityFrameworkCore;
using SchumachersAndAslanovsShop.Models;

namespace SchumachersAndAslanovsShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Part> Part { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<PartCategory> PartCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderItem> ItemList { get; set; }
      
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<CarDescription> CarDescriptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Part>().ToTable("PART", "SYSTEM");
            modelBuilder.Entity<PartCategory>().ToTable("PART_CATEGORY", "SYSTEM");
            modelBuilder.Entity<Car>().ToTable("CARS", "SYSTEM");
            modelBuilder.Entity<CarCategory>().ToTable("CAR_CATEGORY", "SYSTEM");

          
            modelBuilder.Entity<PartCategory>()
                .Property(p => p.CategoryName)
                .HasColumnName("CATEGORY_NAME");
        }
    }
}