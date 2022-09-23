namespace AirsoftShop.Services.Common;

using AirsoftShop.Common.Models;
using Data.Persistence;
using Factories;
using Microsoft.EntityFrameworkCore;
using static AirsoftShop.Common.Constants.Messages;

public abstract class BaseProductService<TEntity, TResult> : IBaseProductService<TEntity, TResult>
    where TEntity : class
    where TResult : class
{
    private readonly IProductFactory<TEntity, TResult> productFactory;

    protected BaseProductService(ApplicationDbContext data, IProductFactory<TEntity, TResult> productFactory)
    {
        this.Context = data;
        this.productFactory = productFactory;
    }

    protected DbSet<TEntity> DbSet => this.Context.Set<TEntity>();

    private ApplicationDbContext Context { get; }

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
}