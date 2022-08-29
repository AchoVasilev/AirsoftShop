namespace AirsoftShop.Services.Services.Order;

using Common.Models;
using Models.Order;

public interface IOrderService
{
    Task<OperationResult<CreateOrderSuccessModel>> CreateOrder(string userClientId, CreateOrderServiceModel model);

    Task<IEnumerable<OrderListServiceModel>> GetClientOrders(string clientId);
    
    Task<OrderDetailsServiceModel?> GetOrderDetails(string clientId, string orderId);
}