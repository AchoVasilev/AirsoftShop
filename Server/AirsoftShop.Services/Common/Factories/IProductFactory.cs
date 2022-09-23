namespace AirsoftShop.Services.Common.Factories;

public interface IProductFactory<TEntity, out TResult>
    where TEntity : class
    where TResult : class
{
    TEntity CreateFromInputModel(IProduct product, string dealerId);

    TResult CreateResultModel(TEntity product);
}