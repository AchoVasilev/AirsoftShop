namespace AirsoftShop.Services.Services.Common.BaseProductService;

using System.Linq.Expressions;
using AirsoftShop.Common.Models;
using Data.Persistence;
using Data.Models.Products;
using Factories;
using Microsoft.EntityFrameworkCore;
using static AirsoftShop.Common.Constants.Messages;

public abstract class BaseProductService<TEntity, TResult> : IBaseProductService<TEntity, TResult>
    where TEntity : Product
    where TResult : class
{
    private readonly IProductFactory<TEntity, TResult> productFactory;

    protected BaseProductService(ApplicationDbContext data, IProductFactory<TEntity, TResult> productFactory)
    {
        this.Context = data;
        this.productFactory = productFactory;
    }

    protected DbSet<TEntity> DbSet => this.Context.Set<TEntity>();

    protected ApplicationDbContext Context { get; }

    public virtual async Task<OperationResult<TResult>> Create(IProduct model, string dealerId)
    {
        var dealerExists = await this.Context.Dealers
            .AnyAsync(x => x.Id == dealerId);

        if (!dealerExists)
        {
            return UserNotDealerMsg;
        }
        
        var subCategoryExists = await this.Context.SubCategories
            .AnyAsync(x => x.Id == model.SubCategoryId);

        if (!subCategoryExists)
        {
            return InvalidSubcategoryErrorMsg;
        }

        var product = this.productFactory.CreateFromInputModel(model, dealerId);

        await this.DbSet.AddAsync(product);
        await this.Context.SaveChangesAsync();

        var resultModel = this.productFactory.CreateResultModel(product);
        
        return resultModel;
    }

    public virtual async Task<OperationResult<TResult>> Edit(IProduct productEntity, string dealerId, Expression<Func<TEntity, bool>> filter)
    {
        var dealer = await this.Context.Dealers
            .FirstOrDefaultAsync(x => x.Id == dealerId);

        if (dealer is null)
        {
            return NotAuthorizedMsg;
        }

        var subCategoryExists = await this.Context.SubCategories
            .AnyAsync(x => x.Id == productEntity.SubCategoryId);

        if (!subCategoryExists)
        {
            return InvalidSubcategoryErrorMsg;
        }

        var item = await this.DbSet
            .FirstOrDefaultAsync(filter);
        if (item is null)
        {
            return InvalidProduct;
        }
        
        item = this.productFactory.CreateUpdatedModel(item, productEntity);
        this.DbSet.Attach(item);
        this.Context.Entry(item).State = EntityState.Modified;

        await this.Context.SaveChangesAsync();
        
        var resultModel = this.productFactory.CreateResultModel(item);
        
        return resultModel;
    }
}