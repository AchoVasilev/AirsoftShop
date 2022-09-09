namespace AirsoftShop.Services.Services.Field;

using AirsoftShop.Common.Models;
using Data.Models;
using Data.Models.Images;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Address;
using Models.City;
using Models.Field;
using static AirsoftShop.Common.Constants.Messages;
public class FieldService : IFieldService
{
    private readonly ApplicationDbContext data;

    public FieldService(ApplicationDbContext data) 
        => this.data = data;

    public async Task<OperationResult<CreatedFieldResultServiceModel>> Create(string dealerId, CreateFieldServiceModel model)
    {
        var city = await this.data.Cities
            .FirstOrDefaultAsync(x => x.Id == model.CityId);
        if (city is null)
        {
            return InvalidCityMsg;
        }

        city.ZipCode = model.ZipCode;
        
        var dealer = await this.data.Dealers
            .FirstOrDefaultAsync(x => x.Id == dealerId);
        if (dealer is null)
        {
            return UserNotDealerMsg;
        }

        var field = new Field()
        {
            Description = model.Description,
            Address = new Address()
            {
                StreetName = model.StreetName,
                CityId = model.CityId,
                City = city,
                DealerId = dealer.Id
            },
            DealerId = dealer.Id,
            Images = model.Images!.Select(x => new Image()
            {
                Url = x.Uri,
                Extension = x.Extension,
                Name = x.Name
            }).ToList(),
        };
        
        dealer.Fields.Add(field);
        await this.data.SaveChangesAsync();
        
        return new CreatedFieldResultServiceModel()
        {
            FieldId = field.Id,
        };
    }

    public async Task<OperationResult<FieldDetailsServiceModel>> Details(int fieldId)
    {
        var field = await this.data.Fields
            .Where(x => x.Id == fieldId)
            .Select(x => new FieldDetailsServiceModel()
            {
                Id = x.Id,
                Description = x.Description,
                DealerId = x.DealerId,
                DealerName = x.Dealer.Name,
                DealerPhone = x.Dealer.PhoneNumber,
                Address = new AddressServiceModel()
                {
                    StreetName = x.Address.StreetName,
                    City = new CityServiceModel()
                    {
                        Id = x.Address.CityId,
                        Name = x.Address.City.Name,
                        ZipCode = x.Address.City.ZipCode
                    }
                },
                ImageUrls = x.Images.Select(img => img.Url ?? img.RemoteImageUrl).ToList()
            })
            .FirstOrDefaultAsync();

        if (field is null)
        {
            return InvalidField;
        }

        return field;
    }
}