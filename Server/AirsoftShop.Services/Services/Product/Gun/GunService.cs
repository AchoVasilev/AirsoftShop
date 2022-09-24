namespace AirsoftShop.Services.Services.Product.Gun;

using AirsoftShop.Common.Models;
using AirsoftShop.Data.Models.Products;
using Data.Persistence;
using AirsoftShop.Services.Models.Product;
using AirsoftShop.Services.Models.Product.Guns;
using Common.BaseProductService;
using Common.Factories;
using Microsoft.EntityFrameworkCore;
using static AirsoftShop.Common.Constants.Messages;

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

    public async Task<GunDetailsServiceModel?> GetDetails(string gunId)
        => await this.DbSet
            .Where(x => x.Id == gunId)
            .Select(x => new GunDetailsServiceModel
            {
                Id = x.Id,
                Name = x.Name,
                Manufacturer = x.Manufacturer,
                Power = x.Power,
                Barrel = x.Barrel,
                Propulsion = x.Propulsion.ToString(),
                Material = x.Material,
                Blowback = x.Blowback,
                Capacity = x.Capacity,
                Speed = x.Speed,
                Color = x.Color,
                Weight = x.Weight,
                Magazine = x.Magazine,
                DealerId = x.DealerId,
                DealerName = x.Dealer.Name,
                DealerUrl = x.Dealer.SiteUrl,
                Firing = x.Firing,
                Length = x.Length,
                Hopup = x.Hopup,
                Price = x.Price,
                Description = x.Description,
                ImageUrl = x.Images.Select(y => y.Url ?? y.RemoteImageUrl).FirstOrDefault()
            })
            .FirstOrDefaultAsync();

 public async Task<OperationResult<OwnerGunListServiceModel>> GetMyProducts(string userId)
    {
        var dealerId = await this.Context.Users
            .Where(x => x.Id == userId)
            .Select(x => x.DealerId)
            .FirstOrDefaultAsync();

        if (dealerId is null)
        {
            return NotAuthorizedMsg;
        }

        var guns = await this.Context.Guns
            .Where(x => x.DealerId == dealerId)
            .Select(x => new OwnerGunListServiceModel()
            {
                Id = x.Id,
                Color = x.Color,
                CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                DealerId = dealerId,
                ImageUrl = x.Images.Select(i => i.Url ?? i.RemoteImageUrl).First(),
                Manufacturer = x.Manufacturer,
                Name = x.Name,
                Price = x.Price,
            })
            .ToListAsync();

        return guns;
    }
    
    public async Task<ICollection<GunViewServiceModel>> GetAllGuns(GunsQueryServiceModel query)
        => await this.QueryAll(query)
            .Select(x => new GunViewServiceModel()
            {
                Id = x.Id,
                Name = x.Name,
                Manufacturer = x.Manufacturer,
                Power = x.Power,
                Barrel = x.Barrel,
                Propulsion = x.Propulsion.ToString(),
                Material = x.Material,
                Blowback = x.Blowback,
                Capacity = x.Capacity,
                Speed = x.Speed,
                Color = x.Color,
                Weight = x.Weight,
                Magazine = x.Magazine,
                DealerId = x.DealerId,
                DealerName = x.Dealer.Name,
                DealerUrl = x.Dealer.SiteUrl,
                Firing = x.Firing,
                Length = x.Length,
                Hopup = x.Hopup,
                Price = x.Price,
                ImageUrl = x.Images.Select(y => y.Url ?? y.RemoteImageUrl).First()
            })
            .ToListAsync();

    public async Task<int> GetAllGunsCount()
        => await this.Context.Guns.CountAsync();

    public async Task<ICollection<string>> GetAllColors()
        => await this.Context.Guns
            .Select(x => x.Color)
            .Distinct()
            .ToListAsync();

    public async Task<ICollection<string>> GetAllDealers()
        => await this.Context.Guns
            .Select(x => x.Dealer.Name)
            .Distinct()
            .ToListAsync();

    public async Task<ICollection<string>> GetAllManufacturers()
        => await this.Context.Guns
            .Select(x => x.Manufacturer)
            .Distinct()
            .ToListAsync();

    public async Task<ICollection<double>> GetAllPowers()
        => await this.Context.Guns
            .Select(x => x.Power)
            .Distinct()
            .ToListAsync();

    private IQueryable<Gun> QueryAll(GunsQueryServiceModel query)
    {
        var gunsQuery = this.Context.Guns
            .AsQueryable();

        if (string.IsNullOrWhiteSpace(query.CategoryName) == false && query.CategoryName != "null")
        {
            gunsQuery = gunsQuery.Where(x => x.SubCategory.Name == query.CategoryName);
        }

        if (query.Manufacturers != null)
        {
            gunsQuery = gunsQuery
                .Where(x => query.Manufacturers.Contains(x.Manufacturer));
        }

        if (query.Dealers != null)
        {
            gunsQuery = gunsQuery
                .Where(x => query.Dealers.Contains(x.Dealer.Name));
        }

        if (query.Colors != null)
        {
            gunsQuery = gunsQuery.Where(x => query.Colors.Contains(x.Color));
        }

        if (query.Powers != null)
        {
            gunsQuery = gunsQuery.Where(x => query.Powers.Contains(x.Power));
        }

        if (string.IsNullOrWhiteSpace(query.CategoryName) == false && query.CategoryName != "null")
        {
            gunsQuery = gunsQuery.Where(x => x.SubCategory.Name == query.CategoryName);
        }

        gunsQuery = query.OrderBy switch
        {
            "newest" => gunsQuery.OrderByDescending(x => x.CreatedOn),
            "alphabetical" => gunsQuery.OrderBy(x => x.Name),
            "priceDown" => gunsQuery.OrderByDescending(x => x.Price),
            "priceUp" => gunsQuery.OrderBy(x => x.Price),
            _ => gunsQuery
        };

        gunsQuery = gunsQuery.Skip((query.Page - 1) * query.ItemsPerPage)
            .Take(query.ItemsPerPage);

        return gunsQuery;
    }
}