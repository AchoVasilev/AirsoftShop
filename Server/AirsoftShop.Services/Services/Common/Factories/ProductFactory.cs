namespace AirsoftShop.Services.Services.Common.Factories;

public abstract class ProductFactory<TEntity, TResult> : IProductFactory<TEntity, TResult>
    where TEntity : class
    where TResult : class
{
    public abstract TEntity CreateFromInputModel(IProduct product, string dealerId);

    public abstract TResult CreateResultModel(TEntity product);
}