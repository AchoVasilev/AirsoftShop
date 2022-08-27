namespace AirsoftShop.Services.Services.Order;

using Common.Models;
using Models.Order;

public interface IOrderService
{
    Task<OperationResult<CreateOrderSuccessModel>> CreateOrder(string userClientId, CreateOrderServiceModel model);
}