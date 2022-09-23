namespace AirsoftShop.Services.Common;

using Data.Models;
using Factories;
using Models.Product;

public interface IGunService : IBaseProductService<Gun, ProductResultModel>
{
    Task<IEnumerable<InitialGunViewModel>> GetNewestEightGuns();
}