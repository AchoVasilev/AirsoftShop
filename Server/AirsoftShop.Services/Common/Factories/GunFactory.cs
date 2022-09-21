namespace AirsoftShop.Services.Common.Factories;

public class GunFactory<T, R> : IProductFactory<T, R>
{
    public T CreateFromInputModel(IProduct product, string dealerId)
    {
        throw new NotImplementedException();
    }

    public R CreateResultModel(T product)
    {
        throw new NotImplementedException();
    }
}