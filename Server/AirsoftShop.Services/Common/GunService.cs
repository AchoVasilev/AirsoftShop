namespace AirsoftShop.Services.Common;

using AirsoftShop.Common.Models;
using Data.Models;
using Data.Models.Enums;
using Data.Models.Images;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Product;

using static AirsoftShop.Common.Constants.Messages;

public class GunService : BaseProductService<Gun, ResultGunServiceModel, CreateGunServiceModel>, IGunService
{
    public GunService(ApplicationDbContext data) : base(data)
    {
    }
    
    public override async Task<OperationResult<ResultGunServiceModel>> CreateGun(CreateGunServiceModel model, string dealerId)
    {
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
}