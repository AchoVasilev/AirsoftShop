namespace AirsoftShop.Services.Common;

using Data.Models;
using Models.Product;

public interface IGunService : IBaseProductService<Gun, ResultGunServiceModel, IGunModel>
{
}