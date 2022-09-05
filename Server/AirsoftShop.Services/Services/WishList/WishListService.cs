namespace AirsoftShop.Services.Services.WishList;

using AirsoftShop.Common.Models;
using Data.Models;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.WishList;

using static AirsoftShop.Common.Constants.Messages;
public class WishListService : IWishListService
{
    private readonly ApplicationDbContext data;

    public WishListService(ApplicationDbContext data)
        => this.data = data;

    public async Task<IEnumerable<WishListServiceModel>> GetItems(string clientId)
        => await this.data.ItemsInWishList
            .Where(x => x.WishList.ClientId == clientId)
            .Select(x => new WishListServiceModel()
            {
                Id = x.GunId,
                Capacity = x.Gun.Capacity,
                Manufacturer = x.Gun.Manufacturer,
                DateAdded = x.CreatedOn.ToString("dd/MM/yyyy"),
                Name = x.Gun.Name,
                Price = x.Gun.Price,
            })
            .ToListAsync();

    public async Task<OperationResult<AddedToWishListServiceModel>> Add(string gunId, string clientId)
    {
        var gunExists = await this.data.Guns
            .AnyAsync(x => x.Id == gunId);

        if (!gunExists)
        {
            return InvalidGun;
        }
        
        var client = await this.data.Clients
            .Where(x => x.Id == clientId)
            .Include(x => x.WishList)
            .FirstAsync();
        
        client.WishList ??= new WishList()
        {
            ClientId = client.Id
        };

        var itemInWishList = new ItemInWishList()
        {
            GunId = gunId
        };

        client.WishList.ItemsInWishList.Add(itemInWishList);

        await this.data.SaveChangesAsync();

        return true;
    }
}