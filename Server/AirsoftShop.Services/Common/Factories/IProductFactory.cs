namespace AirsoftShop.Services.Common.Factories;

public interface IProductFactory<T, R>
{
    T CreateFromInputModel(IProduct product, string dealerId);

    R CreateResultModel(T product);
}