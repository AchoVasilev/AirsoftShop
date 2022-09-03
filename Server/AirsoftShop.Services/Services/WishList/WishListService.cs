namespace AirsoftShop.Services.Services.WishList;

using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.WishList;

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
}