namespace AirsoftShop.Services.Common;

using Data.Models;
using Data.Persistence;
using Factories;
using Microsoft.EntityFrameworkCore;
using Models.Product;

public class GunService : BaseProductService<Gun, ProductResultModel>, IGunService
{
    private const int Take = 8;
    
    public GunService(ApplicationDbContext data, IProductFactory<Gun, ProductResultModel> gunFactory) 
        : base(data, gunFactory)
    {
    }

    public async Task<IEnumerable<InitialGunViewModel>> GetNewestEightGuns()
        => await this.DbSet
            .Select(x => new InitialGunViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Color = x.Color,
                Manufacturer = x.Manufacturer,
                Power = x.Power,
                DealerName = x.Dealer.Name,
                DealerSiteUrl = x.Dealer.SiteUrl,
                ImageUrl = x.Images.Select(i => i.Url ?? i.RemoteImageUrl).First()
            })
            .Take(Take)
            .AsNoTracking()
            .ToListAsync();
}