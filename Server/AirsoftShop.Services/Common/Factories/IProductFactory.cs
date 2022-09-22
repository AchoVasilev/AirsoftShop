namespace AirsoftShop.Services.Common.Factories;

public interface IProductFactory<T, R> : IScopedService
{
    T CreateFromInputModel(IProduct product, string dealerId);

    R CreateResultModel(T product);
}