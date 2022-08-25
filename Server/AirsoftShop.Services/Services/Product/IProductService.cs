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

    Task<ICollection<GunViewServiceModel>> FilterGunsByManufacturer(List<string> query);

    Task<ICollection<GunViewServiceModel>> FilterGunsByDealer(List<string> query);

    Task<ICollection<GunViewServiceModel>> FilterGunsByColor(List<string> query);

    Task<ICollection<GunViewServiceModel>> FilterGunsByPower(GunsQueryServiceModel query);

    Task<ICollection<GunViewServiceModel>> FilterGunsByCategory(GunsQueryServiceModel query);

    Task<ICollection<GunViewServiceModel>> OrderGuns(GunSortModel model);

    Task<ICollection<GunViewServiceModel>> GetAllGuns(GunsQueryServiceModel query);

    Task<int> GetAllGunsCount();

    Task<ICollection<string>> GetAllColors();

    Task<ICollection<string>> GetAllDealers();

    Task<ICollection<string>> GetAllManufacturers();

    Task<ICollection<double>> GetAllPowers();
}