namespace AirsoftShop.Services.Services.Product.Gun;

using AirsoftShop.Common.Models;
using AirsoftShop.Data.Models.Products;
using AirsoftShop.Services.Models.Product;
using AirsoftShop.Services.Models.Product.Guns;
using Common.BaseProductService;

public interface IGunService : IBaseProductService<Gun, ProductResultModel>
{
    Task<IEnumerable<InitialGunViewModel>> GetNewestEightGuns();
    
    Task<GunDetailsServiceModel?> GetDetails(string gunId);

    Task<OperationResult<OwnerGunListServiceModel>> GetMyProducts(string userId);

    Task<ICollection<GunViewServiceModel>?> GetAllGuns(GunsQueryServiceModel query);

    Task<int> GetAllGunsCount();

    Task<ICollection<string>> GetAllColors();

    Task<ICollection<string>> GetAllDealers();

    Task<ICollection<string>> GetAllManufacturers();

    Task<ICollection<double>> GetAllPowers();
}