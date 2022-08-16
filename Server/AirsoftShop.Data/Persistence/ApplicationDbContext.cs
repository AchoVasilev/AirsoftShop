﻿namespace AirsoftShop.Data.Persistence;

using Common.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using Models.Base;
using Models.Images;

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

    private void ApplyAuditInformation()
        => this.ChangeTracker
            .Entries()
            .ToList()
            .ForEach(entry =>
            {
                switch (entry.Entity)
                {
                    case IDeletableEntity<int> deletableEntity:
                        CheckForDeletedEntity(entry, deletableEntity);
                        break;
                    case IDeletableEntity<string> deletableEntity:
                        CheckForDeletedEntity(entry, deletableEntity);
                        break;
                    case IEntity<int> entity:
                        CheckForIEntity(entry, entity);
                        break;
                    case IEntity<string> entity:
                        CheckForIEntity(entry, entity);
                        break;
                }
            });

    private void CheckForIEntity<T>(EntityEntry entry, IEntity<T> entity)
    {
        if (entry.State == EntityState.Added)
        {
            entity.CreatedOn = DateTime.UtcNow;
            entity.CreatedBy = this.currentUserService.GetUserId();
        }
        else if (entry.State == EntityState.Modified)
        {
            entity.ModifiedOn = DateTime.UtcNow;
            entity.ModifiedBy = this.currentUserService.GetUserId();
        }
    }

    private void CheckForDeletedEntity<T>(EntityEntry entry, IDeletableEntity<T> deletableEntity)
    {
        if (entry.State == EntityState.Deleted)
        {
            deletableEntity.DeletedOn = DateTime.UtcNow;
            deletableEntity.DeletedBy = this.currentUserService.GetUserId();
            deletableEntity.IsDeleted = true;

            entry.State = EntityState.Modified;
        }
    }
}