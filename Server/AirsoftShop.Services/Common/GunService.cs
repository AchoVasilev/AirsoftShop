namespace AirsoftShop.Services.Common;

using AirsoftShop.Common.Models;
using Data.Models;
using Data.Models.Enums;
using Data.Models.Images;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Product;
using static AirsoftShop.Common.Constants.Messages;

public class GunService : BaseProductService<Gun, ResultGunServiceModel>, IGunService
{
    public GunService(ApplicationDbContext data) 
        : base(data)
    {
    }
    
    public override async Task<OperationResult<ResultGunServiceModel>> Create(IProduct input, string dealerId)
    {
        var model = (CreateGunServiceModel)input;
        
        var dealer = await this.Context.Dealers
            .FirstOrDefaultAsync(x => x.Id == dealerId);

        if (dealer is null)
        {
            return NotAuthorizedMsg;
        }

        var subCategoryId = await this.Context.SubCategories
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
            Propulsion = Enum.Parse<Propulsion>(model.Propulsion!),
            Power = model.Power,
            Description = model.Description,
            Images = model.Images!.Select(x => new ItemImage()
            {
                Url = x.Uri,
                Name = x.Name,
                Extension = x.Extension
            }).ToList()
        };

        dealer.Guns.Add(gun);
        await this.Context.SaveChangesAsync();

        var result = new ResultGunServiceModel()
        {
            Id = gun.Id
        };

        return result;
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
            .Take(8)
            .AsNoTracking()
            .ToListAsync();
}