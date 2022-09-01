namespace AirsoftShop.Services.Services.Order;

using AirsoftShop.Common.Models;
using Common.Services.Common;
using Models.Order;

public interface IOrderService : ITransientService
{
    Task<OperationResult<CreateOrderSuccessModel>> CreateOrder(string userClientId, CreateOrderServiceModel model);

    Task<IEnumerable<OrderListServiceModel>> GetClientOrders(string clientId);
    
    Task<OrderDetailsServiceModel?> GetOrderDetails(string clientId, string orderId);

}