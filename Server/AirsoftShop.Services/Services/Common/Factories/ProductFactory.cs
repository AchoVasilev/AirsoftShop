namespace AirsoftShop.Services.Services.Common.Factories;

public abstract class ProductFactory<TEntity, TResult> : IProductFactory<TEntity, TResult>
    where TEntity : class
    where TResult : class
{
    public abstract TEntity CreateFromInputModel(IProduct product, string dealerId);
    public TResult CreateResultModel(TEntity product)
    {
        throw new NotImplementedException();
    }

    public TEntity CreateUpdatedModel(TEntity item, IProduct product)
    {
        throw new NotImplementedException();
    }

    public abstract TResult CreateResultModel(TEntity item, IProduct product);
}