namespace AirsoftShop.Services.Services.Cart;

using Common.Models;
using Models.Cart;

public interface ICartService
{
    Task<OperationResult<AddedToCartResultServiceModel>> Add(string userClientId, string gunId);
    
    Task<IEnumerable<CartViewServiceModel>> GetItemsInCart(string userClientId);
    
    Task<bool> DeleteItemById(string userClientId, string itemId);

    Task<CartDeliveryDataServiceModel> GetCartDeliveryData();

    Task<bool> ClearCart(string clientId);
    
    Task<NavCartServiceModel> GetCartData(string clientId);
}