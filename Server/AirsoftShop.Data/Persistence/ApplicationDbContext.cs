namespace AirsoftShop.Data.Persistence;

using Common.Services;
using Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Models.Base;
using Models.Images;
using Models.Products;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly ICurrentUserService currentUserService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService)
        : base(options)
        => this.currentUserService = currentUserService;


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
    
    public DbSet<Clothing> Clothings { get; init; }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.ApplyAuditInformation();

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        this.ApplyAuditInformation();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

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

        RemoveCascadeDelete(builder);

        ApplySoftDeleteQueryFilter(builder);

        base.OnModelCreating(builder);
    }

    private static void RemoveCascadeDelete(ModelBuilder builder)
    {
        var entityTypes = builder.Model.GetEntityTypes().ToList();
        var foreignKeys = entityTypes
            .SelectMany(e => e.GetForeignKeys()
                .Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));

        foreach (var foreignKey in foreignKeys)
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    private static void ApplySoftDeleteQueryFilter(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(IDeletableEntity<int>).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddSoftDeletableQueryFilter<int>();
            }
            else if (typeof(IDeletableEntity<string>).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddSoftDeletableQueryFilter<string>();
            }
        }
    }

    private void ApplyAuditInformation()
        => this.ChangeTracker
            .Entries()
            .ToList()
            .ForEach(entry =>
            {
                if (entry.Entity is IDeletableEntity<int> intDeletableEntity)
                {
                    this.CheckForDeletedEntity(entry, intDeletableEntity);
                }

                if (entry.Entity is IDeletableEntity<string> stringDeletableEntity)
                {
                    this.CheckForDeletedEntity(entry, stringDeletableEntity);
                }

                if (entry.Entity is IEntity<int> intEntity)
                {
                    this.CheckForIEntity(entry, intEntity);
                }

                if (entry.Entity is IEntity<string> stringEntity)
                {
                    this.CheckForIEntity(entry, stringEntity);
                }
            });

    private void CheckForIEntity<T>(EntityEntry entry, IEntity<T> entity)
    {
        if (entry.State == EntityState.Added)
        {
            entity.CreatedOn = DateTime.UtcNow;
            entity.CreatedBy = this.currentUserService.GetUserId() != null
                ? this.currentUserService.GetUserId()
                : "admin";
        }
        else if (entry.State == EntityState.Modified)
        {
            entity.ModifiedOn = DateTime.UtcNow;
            entity.CreatedBy = this.currentUserService.GetUserId() != null
                ? this.currentUserService.GetUserId()
                : "admin";
        }
    }

    private void CheckForDeletedEntity<T>(EntityEntry entry, IDeletableEntity<T> deletableEntity)
    {
        if (entry.State == EntityState.Deleted)
        {
            deletableEntity.DeletedOn = DateTime.UtcNow;
            deletableEntity.DeletedBy = this.currentUserService.GetUserId() != null
                ? this.currentUserService.GetUserId()
                : "admin";
            deletableEntity.IsDeleted = true;

            entry.State = EntityState.Modified;
        }
    }
}