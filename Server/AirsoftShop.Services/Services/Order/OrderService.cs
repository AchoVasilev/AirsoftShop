namespace AirsoftShop.Services.Services.Order;

using Common.Models;
using Data.Models;
using Data.Models.Enums;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Order;

using static Common.Constants.Messages;
public class OrderService : IOrderService
{
    private readonly ApplicationDbContext data;

    public OrderService(ApplicationDbContext data) 
        => this.data = data;

    public async Task<OperationResult<CreateOrderSuccessModel>> CreateOrder(string userClientId, CreateOrderServiceModel model)
    {
        var user = await this.data.Clients
            .FirstOrDefaultAsync(x => x.Id == userClientId);

        if (user is null)
        {
            return UserNotClientMsg;
        }
        
        var courier = await this.data.Couriers
            .FirstOrDefaultAsync(x => x.Id == model.CourierId);

        if (courier is null)
        {
            return InvalidCourier;
        }

        var guns = new List<Gun>();
        for (var i = 0; i < model.GunsIds.Count; i++)
        {
            var gun = await this.data.Guns
                .FirstOrDefaultAsync(x => x.Id == model.GunsIds[i]);

            if (gun is null)
            {
                continue;
            }
            
            guns.Add(gun);
        }

        var dealers = await this.data.Dealers
            .ToListAsync();

        foreach (var dealer in dealers)
        {
            var order = new Order();

            foreach (var gun in guns)
            {
                if (dealer.Id == gun.DealerId)
                {
                    order.ClientId = user.Id;
                    order.PaymentType = model.PaymentType == "cash" ? PaymentType.Cash : PaymentType.Card;
                    order.OrderStatus = OrderStatus.Processing;
                    order.Dealers.Add(dealer);
                    order.Guns.Add(gun);
                    order.TotalPrice += gun.Price;
                    order.CourierId = courier.Id;
                }
            }

            if (order.Guns.Count > 0)
            {
                user.Orders.Add(order);
            }
        }

        await this.data.SaveChangesAsync();

        var result = new CreateOrderSuccessModel()
        {
            OrdersCount = user.Orders.Count
        };
        
        return result;
    }
}