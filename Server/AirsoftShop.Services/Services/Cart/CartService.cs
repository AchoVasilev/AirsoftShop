namespace AirsoftShop.Services.Services.Cart;

using AirsoftShop.Common.Models;
using Data.Models;
using Data.Models.Enums;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Cart;
using Models.Courier;
using static AirsoftShop.Common.Constants.Messages;

public class CartService : ICartService
{
    private readonly ApplicationDbContext data;

    public CartService(ApplicationDbContext data)
        => this.data = data;

    public async Task<OperationResult<AddedToCartResultServiceModel>> Add(string userClientId, string gunId)
    {
        var client = await this.data.Clients
            .FirstOrDefaultAsync(x => x.Id == userClientId);

        if (client is null)
        {
            return NotAuthorizedMsg;
        }

        if (client.CartId is null)
        {
            client.Cart = new Cart()
            {
                ClientId = client.Id
            };
        }

        var gun = await this.data.Guns.FirstOrDefaultAsync(x => x.Id == gunId);
        if (gun is null)
        {
            return InvalidGun;
        }

        client.Cart.Guns.Add(gun);
        await this.data.SaveChangesAsync();

        var result = new AddedToCartResultServiceModel()
        {
            CartId = client.CartId!,
            ItemsCount = client.Cart.Guns.Count,
            GunId = gun.Id
        };

        return result;
    }

    public async Task<OperationResult<AddedToCartResultServiceModel>> Add(string userClientId, IEnumerable<string> gunIds)
    {
        var client = await this.data.Clients
            .FirstOrDefaultAsync(x => x.Id == userClientId);

        if (client is null)
        {
            return NotAuthorizedMsg;
        }

        if (client.CartId is null)
        {
            client.Cart = new Cart()
            {
                ClientId = client.Id
            };
        }

        var guns = await this.data.Guns
            .Where(x => gunIds.Contains(x.Id))
            .ToListAsync();

        foreach (var gun in guns)
        {
            client.Cart.Guns.Add(gun);
        }

        await this.data.SaveChangesAsync();

        var result = new AddedToCartResultServiceModel()
        {
            CartId = client.CartId!,
            ItemsCount = client.Cart.Guns.Count
        };

        return result;
    }

    public async Task<IEnumerable<CartViewServiceModel>> GetItemsInCart(string userClientId)
    {
        var cart = await this.data.Clients
            .Where(x => x.Id == userClientId)
            .Include(x => x.Cart)
            .ThenInclude(x => x.Guns)
            .ThenInclude(x => x.Images)
            .FirstOrDefaultAsync();

        var gunsInCart = cart?.Cart.Guns
            .Select(cartGun => new CartViewServiceModel()
            {
                Id = cartGun.Id,
                Color = cartGun.Color,
                ImageUrl = cartGun.Images.Select(x => x.Url ?? x.RemoteImageUrl).FirstOrDefault()!,
                Manufacturer = cartGun.Manufacturer,
                Name = cartGun.Name,
                Price = cartGun.Price,
            })
            .ToList();

        return gunsInCart ?? new List<CartViewServiceModel>();
    }

    public async Task<bool> DeleteItemById(string userClientId, string itemId)
    {
        var client = await this.data.Clients
            .Where(x => x.Id == userClientId)
            .Include(x => x.Cart)
            .ThenInclude(x => x.Guns)
            .FirstOrDefaultAsync();

        if (client?.CartId is null)
        {
            return false;
        }

        var gun = client.Cart.Guns.FirstOrDefault(x => x.Id == itemId);
        if (gun is null)
        {
            return false;
        }

        var result = client.Cart.Guns.Remove(gun);
        await this.data.SaveChangesAsync();

        return result;
    }

    public async Task<CartDeliveryDataServiceModel> GetCartDeliveryData()
    {
        var couriers = await this.data.Couriers
            .Select(x => new CourierServiceModel()
            {
                Id = x.Id,
                DeliveryDays = x.DeliveryDays,
                DeliveryPrice = x.DeliveryPrice,
                ImageUrl = x.Image.Url ?? x.Image.RemoteImageUrl,
                Name = x.Name
            })
            .ToListAsync();

        var deliveryData = new CartDeliveryDataServiceModel();

        foreach (var courier in couriers)
        {
            deliveryData.Couriers.Add(courier);
        }

        deliveryData.CashPayment = PaymentType.Cash.ToString();
        deliveryData.CardPayment = PaymentType.Card.ToString();

        return deliveryData;
    }

    public async Task<bool> ClearCart(string clientId)
    {
        var client = await this.data.Clients
            .Where(x => x.Id == clientId)
            .Include(x => x.Cart)
            .ThenInclude(x => x.Guns)
            .FirstOrDefaultAsync();

        client?.Cart.Guns.Clear();

        if (client?.Cart.Guns.Count == 0)
        {
            await this.data.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public async Task<NavCartServiceModel> GetCartData(string clientId)
    {
        var model = await this.data.Clients
            .Where(x => x.Id == clientId)
            .Select(x => new NavCartServiceModel()
            {
                TotalPrice = x.Cart.Guns.Sum(g => g.Price),
                ItemsCount = x.Cart.Guns.Count
            })
            .FirstOrDefaultAsync();

        return model ?? new NavCartServiceModel()
        {
            ItemsCount = 0,
            TotalPrice = 0
        };
    }
}