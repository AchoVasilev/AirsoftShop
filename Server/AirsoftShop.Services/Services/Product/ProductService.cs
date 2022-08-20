namespace AirsoftShop.Services.Services.Product;

using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Products;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext data;

    public ProductService(ApplicationDbContext data) 
        => this.data = data;

    public async Task<IEnumerable<InitialGunViewModel>> GetNewestEightGuns()
        => await this.data.Guns
            .Select(x => new InitialGunViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Color = x.Color,
                Manufacturer = x.Manufacturer,
                Power = x.Power,
                DealerName = x.Dealer.Name,
                DealerSiteUrl = x.Dealer.SiteUrl,
                ImageUrl = x.Images.Select(i => i.Url).FirstOrDefault()
            })
            .AsNoTracking()
            .ToListAsync();
}