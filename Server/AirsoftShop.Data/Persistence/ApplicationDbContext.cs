namespace AirsoftShop.Data.Persistence;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Images;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Address> Addresses { get; init; }

    public DbSet<Cart> Carts { get; init; }
    
    public DbSet<Category> Categories { get; init; }

    public DbSet<City> Cities { get; init; }

    public DbSet<Client> Clients { get; init; }

    public DbSet<Courier> Couriers { get; init; }

    public DbSet<Dealer> Dealers { get; init; }

    public DbSet<Field> Fields { get; init; }

    public DbSet<Gun> Guns { get; init; }

    public DbSet<Image> Images { get; init; }
    
    public DbSet<CategoryImage> CategoryImages { get; init; }
    
    public DbSet<ItemImage> ItemImages { get; init; }

    public DbSet<ItemInWishList> ItemsInWishList { get; init; }

    public DbSet<Order> Orders { get; init; }

    public DbSet<SubCategory> SubCategories { get; init; }

    public DbSet<WishList> WishLists { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Gun>()
            .Property(x => x.Price)
            .HasPrecision(14, 2);

        builder.Entity<Gun>()
            .Property(x => x.Weight)
            .HasPrecision(14, 2);

        builder.Entity<Gun>()
            .Property(x => x.Power)
            .HasPrecision(14, 2);

        builder.Entity<Courier>()
            .Property(x => x.DeliveryPrice)
            .HasPrecision(14, 2);

        builder.Entity<Order>()
            .Property(x => x.TotalPrice)
            .HasPrecision(14, 2);

        var entityTypes = builder.Model.GetEntityTypes().ToList();
        var foreignKeys = entityTypes
            .SelectMany(e => e.GetForeignKeys()
                .Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));

        foreach (var foreignKey in foreignKeys)
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }

        base.OnModelCreating(builder);
    }
}
