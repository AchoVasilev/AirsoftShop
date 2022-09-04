namespace AirsoftShop.Services.Services.WishList;

using AirsoftShop.Common.Models;
using Common;
using Models.WishList;

public interface IWishListService : ITransientService
{
    Task<IEnumerable<WishListServiceModel>> GetItems(string clientId);

    Task<OperationResult<AddedToWishListServiceModel>> Add(string gunId, string clientId);
}