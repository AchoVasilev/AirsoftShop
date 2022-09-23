namespace AirsoftShop.Services.Services.Product.Gun;

using AirsoftShop.Common.Models;
using Common;
using Models.Product.Guns;

public interface IGunService : ITransientService
{
    Task<IEnumerable<InitialGunViewModel>> GetNewestEightGuns();
    
    Task<OperationResult<ResultGunServiceModel>> CreateGun (CreateGunServiceModel model, string dealerId);

    Task<GunDetailsServiceModel?> GetDetails(string gunId);
    
    Task<OperationResult<ResultGunServiceModel>> Edit(string dealerId, EditGunServiceModel model);

    Task<OperationResult<ResultGunServiceModel>> DeleteGun(string gunId, string dealerId);

    Task<OperationResult<OwnerGunListServiceModel>> GetMyProducts(string userId);

    Task<ICollection<GunViewServiceModel>> GetAllGuns(GunsQueryServiceModel query);

    Task<int> GetAllGunsCount();

    Task<ICollection<string>> GetAllColors();

    Task<ICollection<string>> GetAllDealers();

    Task<ICollection<string>> GetAllManufacturers();

    Task<ICollection<double>> GetAllPowers();
}