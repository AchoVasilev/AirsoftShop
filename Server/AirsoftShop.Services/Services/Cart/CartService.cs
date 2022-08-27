namespace AirsoftShop.Services.Services.Cart;

using Common.Models;
using Couriers;
using Data.Models;
using Data.Models.Enums;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Cart;
using static Common.Constants.Messages;

public class CartService : ICartService
{
    private readonly ApplicationDbContext data;

    public CartService(ApplicationDbContext data)
        => this.data = data;

    public async Task<OperationResult<AddedToCartResultServiceModel>> Add(string userClientId, string gunId)
    {
        var gun = await this.data.Guns.FirstOrDefaultAsync(x => x.Id == gunId);
        var client = await this.data.Clients
            .FirstOrDefaultAsync(x => x.Id == userClientId);

        if (gun is null)
        {
            return InvalidGun;
        }

        if (client is null)
        {
            return NotAuthorizedMsg;
        }

        if (client.CartId is null)
        {
            client.Cart = new Cart();
        }

        client.Cart.Guns.Add(gun);
        await this.data.SaveChangesAsync();

        var result = new AddedToCartResultServiceModel()
        {
            CartId = client.CartId,
            ItemsCount = client.Cart.Guns.Count,
            GunId = gun.Id
        };

        return result;
    }

    public async Task<IEnumerable<CartViewServiceModel>> GetItemsInCart(string userClientId)
    {
        var cart = await this.data.Carts
            .Where(x => x.ClientId == userClientId)
            .Include(x => x.Guns)
            .FirstOrDefaultAsync();

        var gunsInCart = cart.Guns
            .Select(cartGun => new CartViewServiceModel()
            {
                Id = cartGun.Id,
                Color = cartGun.Color,
                ImageUrl = cartGun.Images.Select(x => x.Url).FirstOrDefault(),
                Manufacturer = cartGun.Manufacturer,
                Name = cartGun.Name,
                Price = cartGun.Price,
            })
            .ToList();

        return gunsInCart;
    }

    public async Task<bool> DeleteItemById(string userClientId, string itemId)
    {
        var client = await this.data.Clients
            .Where(x => x.Id == userClientId)
            .Include(x => x.Cart)
            .ThenInclude(x => x.Guns)
            .FirstAsync();

        if (client.CartId is null)
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
}