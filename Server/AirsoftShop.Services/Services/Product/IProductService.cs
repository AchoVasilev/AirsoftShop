namespace AirsoftShop.Services.Services.Product;

using Common.Models;
using Models.Products;

public interface IProductService
{
    Task<IEnumerable<InitialGunViewModel>> GetNewestEightGuns();
    
    Task<OperationResult<ResultGunServiceModel>> CreateGun (CreateGunServiceModel model, string dealerId);

    Task<GunDetailsServiceModel?> GetDetails(string gunId);
    
    Task<OperationResult<ResultGunServiceModel>> Edit(string dealerId, EditGunServiceModel model);

    Task<OperationResult<ResultGunServiceModel>> DeleteGun(string gunId, string dealerId);
}