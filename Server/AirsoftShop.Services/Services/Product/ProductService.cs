namespace AirsoftShop.Services.Services.Product;

using Common.Models;
using Data.Models;
using Data.Models.Enums;
using Data.Models.Images;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Products;
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

    public async Task<OperationResult<CreatedGunServiceModel>> CreateGun(CreateGunServiceModel model, string dealerId)
    {
        var dealer = await this.data.Dealers
            .FirstOrDefaultAsync(x => x.Id == dealerId);

        if (dealer is null)
        {
            return InvalidUserMsg;
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

        var result = new CreatedGunServiceModel()
        {
            Id = gun.Id,
            Name = gun.Name
        };
        
        return result;
    }
}