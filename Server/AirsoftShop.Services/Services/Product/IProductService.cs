namespace AirsoftShop.Services.Services.Product;

using Models.Products;

public interface IProductService
{
    Task<IEnumerable<InitialGunViewModel>> GetNewestEightGuns();
}