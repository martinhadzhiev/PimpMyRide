namespace PimpMyRide.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class PimpMyRideDbContext : IdentityDbContext<User>
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public PimpMyRideDbContext(DbContextOptions<PimpMyRideDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Order>()
                .HasMany(o => o.Parts)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Entity<User>()
                .HasMany(u => u.MyOrders)
                .WithOne(o => o.Buyer)
                .HasForeignKey(o => o.BuyerId);

            builder
                .Entity<User>()
                .HasMany(u => u.MySales)
                .WithOne(o => o.Seller)
                .HasForeignKey(o => o.SellerId);

            builder
                .Entity<User>()
                .HasMany(u => u.Cars)
                .WithOne(c => c.Owner)
                .HasForeignKey(c => c.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<Car>()
                .HasMany(c => c.Parts)
                .WithOne(p => p.Car)
                .HasForeignKey(p => p.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
