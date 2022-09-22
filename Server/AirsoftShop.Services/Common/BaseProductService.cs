namespace AirsoftShop.Services.Common;

using AirsoftShop.Common.Models;
using Data.Persistence;
using Factories;
using Microsoft.EntityFrameworkCore;
using static AirsoftShop.Common.Constants.Messages;

public abstract class BaseProductService<T, R> : IBaseProductService<T, R>
    where T : class
    where R : class
{
    private readonly IProductFactory<T, R> productFactory;
    public BaseProductService(ApplicationDbContext data, IProductFactory<T, R> productFactory)
    {
        this.Context = data;
        this.productFactory = productFactory;
    }

    protected DbSet<T> DbSet => this.Context.Set<T>();

    protected ApplicationDbContext Context { get; }

    public virtual async Task<OperationResult<R>> Create(IProduct model, string dealerId)
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