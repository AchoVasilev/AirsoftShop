namespace AirsoftShop.Services.Services.WishList;

using Common;
using Models.WishList;

public interface IWishListService : ITransientService
{
    Task<IEnumerable<WishListServiceModel>> GetItems(string clientId);
}