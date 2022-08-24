namespace AirsoftShop.Services.Services.Product;

using Common.Models;
using Models.Products;

public interface IProductService
{
    Task<IEnumerable<InitialGunViewModel>> GetNewestEightGuns();
    
    Task<OperationResult<CreatedGunServiceModel>> CreateGun (CreateGunServiceModel model, string dealerId);
}