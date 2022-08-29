namespace AirsoftShop.Services.Services.Order;

using Common.Models;
using Data.Models;
using Data.Models.Enums;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Courier;
using Models.Order;
using Models.Product;
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

    public async Task<IEnumerable<OrderListServiceModel>> GetClientOrders(string clientId) 
        => await this.data.Orders
            .Where(x => x.ClientId == clientId)
            .Select(x => new OrderListServiceModel()
            {
                OrderId = x.Id,
                TotalPrice = x.TotalPrice,
                CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                Gun = x.Guns.Select(g => new OrderGunViewServiceModel()
                {
                    Manufacturer = g.Manufacturer,
                    Id = g.Id,
                    Color = g.Color,
                    Name = g.Name,
                    ImageUrl = g.Images.Select(i => i.Url ?? i.RemoteImageUrl).First(),
                    Price = g.Price,
                }).ToList()
            }).ToListAsync();

    public async Task<OrderDetailsServiceModel?> GetOrderDetails(string clientId, string orderId)
        => await this.data.Orders
            .Where(x => x.Id == orderId && x.ClientId == clientId)
            .OrderByDescending(x => x.CreatedOn)
            .Select(x => new OrderDetailsServiceModel()
            {
                Id = x.Id,
                PaymentType = x.PaymentType.ToString(),
                OrderStatus = x.OrderStatus.ToString(),
                TotalPrice = x.TotalPrice,
                Courier = new CourierOrderViewServiceModel()
                {
                    DeliveryPrice = x.Courier.DeliveryPrice,
                    Name = x.Courier.Name
                },
                Guns = x.Guns.Select(g => new OrderDetailsGunServiceModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    Price = g.Price,
                    DealerName = g.Dealer.Name,
                    Manufacturer = g.Manufacturer,
                    Color = g.Color,
                    ImageUrl = g.Images.Select(i => i.RemoteImageUrl ?? i.Url).First()
                }).ToList()
            })
            .FirstOrDefaultAsync();
}