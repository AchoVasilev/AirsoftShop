namespace AirsoftShop.Services.Services.Common.Factories;

using IProduct = IProduct;

public interface IProductFactory<TEntity, out TResult>
    where TEntity : class
    where TResult : class
{
    TEntity CreateFromInputModel(IProduct product, string dealerId);

    TResult CreateResultModel(TEntity product);

    TEntity CreateUpdatedModel(TEntity item, IProduct product);
}