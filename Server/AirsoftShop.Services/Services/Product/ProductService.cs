namespace AirsoftShop.Services.Services.Product;

using Common.Models;
using Data.Models;
using Data.Models.Enums;
using Data.Models.Images;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Product;
using static Common.Constants.Messages;
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

    public async Task<OperationResult<ResultGunServiceModel>> CreateGun(CreateGunServiceModel model, string dealerId)
    {
        var dealer = await this.data.Dealers
            .FirstOrDefaultAsync(x => x.Id == dealerId);

        if (dealer is null)
        {
            return NotAuthorizedMsg;
        }
        
        var subCategoryId = await this.data.SubCategories
            .Where(x => x.Name == model.SubCategoryName)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        if (subCategoryId == 0)
        {
            return InvalidSubcategoryErrorMsg;
        }

        var gun = new Gun()
        {
            Name = model.Name,
            Magazine = model.Magazine,
            Manufacturer = model.Manufacturer,
            Material = model.Material,
            Barrel = model.Barrel,
            Blowback = model.Blowback,
            Capacity = model.Capacity,
            Color = model.Color,
            SubCategoryId = subCategoryId,
            Firing = model.Firing,
            Hopup = model.Hopup,
            Weight = model.Weight,
            Length = model.Length,
            Speed = model.Speed,
            Price = model.Price,
            Propulsion = Enum.Parse<Propulsion>(model.Propulsion),
            Power = model.Power,
            Images = model.ImageIds.Select(x => new ItemImage()
            {
                Id = x
            }).ToList(),
        };

        dealer.Guns.Add(gun);
        await this.data.SaveChangesAsync();

        var result = new ResultGunServiceModel()
        {
            Id = gun.Id,
            Name = gun.Name
        };
        
        return result;
    }

    public async Task<GunDetailsServiceModel?> GetDetails(string gunId)
        => await this.data.Guns
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
                ImageUrls = x.Images.Select(y => y.Url).ToList()
            })
            .FirstOrDefaultAsync();

    public async Task<OperationResult<ResultGunServiceModel>> Edit(string dealerId, EditGunServiceModel model)
    {
        var dealer = await this.data.Dealers
            .FirstOrDefaultAsync(x => x.Id == dealerId);

        if (dealer is null)
        {
            return NotAuthorizedMsg;
        }
        
        var subCategoryId = await this.data.SubCategories
            .Where(x => x.Name == model.SubCategoryName)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();

        if (subCategoryId == 0)
        {
            return InvalidSubcategoryErrorMsg;
        }
        
        var gun = await this.data.Guns
            .FirstOrDefaultAsync(x => x.Name == model.Name && dealer.Id == x.DealerId);
        
        if (gun is null)
        {
            return InvalidGun;
        }
        
        gun.Name = model.Name;
        gun.Magazine = model.Magazine;
        gun.Manufacturer = model.Manufacturer;
        gun.Material = model.Material;
        gun.Barrel = model.Barrel;
        gun.Blowback = model.Blowback;
        gun.Capacity = model.Capacity;
        gun.Color = model.Color;
        gun.SubCategoryId = subCategoryId;
        gun.Firing = model.Firing;
        gun.Hopup = model.Hopup;
        gun.Weight = model.Weight;
        gun.Length = model.Length;
        gun.Speed = model.Speed;
        gun.Price = model.Price;
        gun.Propulsion = Enum.Parse<Propulsion>(model.Propulsion);
        gun.Power = model.Power;
        
        await this.data.SaveChangesAsync();

        var result = new ResultGunServiceModel()
        {
            Name = gun.Name,
            Id = gun.Id
        };

        return result;
    }

    public async Task<OperationResult<ResultGunServiceModel>> DeleteGun(string gunId, string dealerId)
    {
        var gun = await this.data.Guns
            .FirstOrDefaultAsync(x => x.Id == gunId);

        if (gun is null)
        {
            return InvalidGun;
        }

        if (gun.DealerId != dealerId)
        {
            return NotAuthorizedMsg;
        }

        var result = new ResultGunServiceModel()
        {
            Id = gun.Id,
            Name = gun.Name
        };
        
        this.data.Remove(gun);
        await this.data.SaveChangesAsync();

        return result;
    }
    
      public async Task<ICollection<GunViewServiceModel>> FilterGunsByManufacturer(List<string> query)
            => await this.data.Guns
                    .Where(x => query.Contains(x.Manufacturer))
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
                        ImageUrls = x.Images.Select(y => y.Url).ToList()
                    })
                    .ToListAsync();

        public async Task<ICollection<GunViewServiceModel>> FilterGunsByDealer(List<string> query)
            => await this.data.Guns
                    .Where(x => query.Contains(x.Dealer.Name))
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
                        ImageUrls = x.Images.Select(y => y.Url).ToList()
                    })
                    .ToListAsync();

        public async Task<ICollection<GunViewServiceModel>> FilterGunsByColor(List<string> query)
            => await this.data.Guns
                    .Where(x => query.Contains(x.Color))
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
                        ImageUrls = x.Images.Select(y => y.Url).ToList()
                    })
                    .ToListAsync();

        public async Task<ICollection<GunViewServiceModel>> FilterGunsByPower(GunsQueryServiceModel query)
            => await this.QueryGuns(query)
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
                    ImageUrls = x.Images.Select(y => y.Url).ToList()
                })
                    .ToListAsync();

        public async Task<ICollection<GunViewServiceModel>> FilterGunsByCategory(GunsQueryServiceModel query)
            => await this.QueryGuns(query)
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
                    ImageUrls = x.Images.Select(y => y.Url).ToList()
                })
                    .ToListAsync();
        
        public async Task<OperationResult<OwnerGunListServiceModel>> GetMyProducts(string userId)
        {
            var dealerId = await this.data.Users
                .Where(x => x.Id == userId)
                .Select(x => x.DealerId)
                .FirstOrDefaultAsync();

            if (dealerId is null)
            {
                return NotAuthorizedMsg;
            }

            var guns = await this.data.Guns
                .Where(x => x.DealerId == dealerId)
                .Select(x => new OwnerGunListServiceModel()
                {
                    Id = x.Id,
                    Color = x.Color,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                    DealerId = dealerId,
                    ImageUrl = x.Images.Select(i => i.Url).First(),
                    Manufacturer = x.Manufacturer,
                    Name = x.Name,
                    Price = x.Price,
                })
                .ToListAsync();

            return guns;
        }

        public async Task<ICollection<GunViewServiceModel>> OrderGuns(GunSortModel model)
            => await this.QuerySortGuns(model)
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
                    ImageUrls = x.Images.Select(y => y.Url).ToList()
                })
                        .ToListAsync();

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
                    ImageUrls = x.Images.Select(y => y.Url).ToList()
                })
                        .ToListAsync();

        public async Task<int> GetAllGunsCount()
            => await this.data.Guns.CountAsync();

        public async Task<ICollection<string>> GetAllColors()
            => await this.data.Guns
                        .Select(x => x.Color)
                        .Distinct()
                        .ToListAsync();

        public async Task<ICollection<string>> GetAllDealers()
            => await this.data.Guns
                        .Select(x => x.Dealer.Name)
                        .Distinct()
                        .ToListAsync();

        public async Task<ICollection<string>> GetAllManufacturers()
            => await this.data.Guns
                        .Select(x => x.Manufacturer)
                        .Distinct()
                        .ToListAsync();

        public async Task<ICollection<double>> GetAllPowers()
          => await this.data.Guns
                      .Select(x => x.Power)
                      .Distinct()
                      .ToListAsync();

        private IQueryable<Gun> QuerySortGuns(GunSortModel query)
        {
            var gunsQuery = this.data.Guns
                .AsQueryable();

            if (string.IsNullOrWhiteSpace(query.CategoryName) == false && query.CategoryName != "null")
            {
                gunsQuery = gunsQuery.Where(x => x.SubCategory.Name == query.CategoryName);
            }

            if (string.IsNullOrWhiteSpace(query.OrderBy) == false && query.OrderBy != "null")
            {
                gunsQuery = query.OrderBy switch
                {
                    "newest" => gunsQuery.OrderByDescending(x => x.CreatedOn),
                    "alphabetical" => gunsQuery.OrderBy(x => x.Name),
                    "priceDown" => gunsQuery.OrderByDescending(x => x.Price),
                    "priceUp" => gunsQuery.OrderBy(x => x.Price),
                    _ => gunsQuery
                };
            }

            if (query.Count != null)
            {
                gunsQuery = gunsQuery.Take((int)query.Count);
            }

            return gunsQuery;
        }

        private IQueryable<Gun> QueryAll(GunsQueryServiceModel query)
        {
            var gunsQuery = this.data.Guns
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

        private IQueryable<Gun> QueryGuns(GunsQueryServiceModel query)
        {
            var gunsQuery = this.data.Guns
                .AsQueryable();

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

            return gunsQuery.OrderByDescending(x => x.CreatedOn);
        }
}