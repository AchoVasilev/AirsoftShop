namespace AirsoftShop.Services.Services.Cart;

using AirsoftShop.Common.Models;
using Common;
using Models.Cart;

public interface ICartService : ITransientService
{
    Task<OperationResult<AddedToCartResultServiceModel>> Add(string userClientId, string gunId);

    Task<OperationResult<AddedToCartResultServiceModel>> Add(string userClientId, IEnumerable<string> gunIds);

    Task<IEnumerable<CartViewServiceModel>> GetItemsInCart(string userClientId);

    Task<bool> DeleteItemById(string userClientId, string itemId);

    Task<CartDeliveryDataServiceModel> GetCartDeliveryData();

    Task<bool> ClearCart(string clientId);

    Task<NavCartServiceModel> GetCartData(string clientId);
}