namespace AirsoftShop.Services.Services.Dealer;

using AirsoftShop.Common.Models;
using AirsoftShop.Data.Models;
using Data.Persistence;
using Models.Address;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.City;
using Models.Dealer;
using static AirsoftShop.Common.Constants.Messages;

public class DealerService : IDealerService
{
    private readonly ApplicationDbContext data;
    private readonly UserManager<ApplicationUser> userManager;
    
    public DealerService(ApplicationDbContext data, UserManager<ApplicationUser> userManager)
    {
        this.data = data;
        this.userManager = userManager;
    }

    public async Task<OperationResult<DealerResultServiceModel>> CreateDealer(CreateDealerServiceModel model, string imageId)
    {
        var city = await this.data.Cities
            .FirstOrDefaultAsync(x => x.Name == model.CityName);

        if (city is null)
        {
            return InvalidCityMsg;
        }

        var applicationUser = new ApplicationUser
        {
            Email = model.Email,
            UserName = model.Username,
            ImageId = imageId,
            Dealer = new Dealer
            {
                Name = model.Name,
                DealerNumber = model.DealerNumber,
                Email = model.Email,
                PhoneNumber = model.Phone,
                Address = new Address
                {
                    StreetName = model.StreetName,
                    CityId = city.Id
                },
                SiteUrl = model.SiteUrl
            }
        };

        var result = await this.userManager.CreateAsync(applicationUser, model.Password);

        if (result.Succeeded)
        {
            await this.data.SaveChangesAsync();
            var dealerResult = new DealerResultServiceModel()
            {
                UserId = applicationUser.Id,
                DealerId = applicationUser.DealerId,
                DealerUserName = applicationUser.Dealer.Name
            };
            
            return dealerResult;
        }

        return result.Errors
            .Select(error => error.Description)
            .ToList();
    }

    public async Task<OperationResult<UserDealerServiceModel>> Profile(string userId)
    {
        var user = await this.userManager
            .Users
            .Include(x => x.Image)
            .Include(x => x.Dealer)
            .ThenInclude(x => x.Address)
            .ThenInclude(x => x.City)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (user is null)
        {
            return UserNotDealerMsg;
        }

        var model = new UserDealerServiceModel()
        {
            UserId = user.Id,
            DealerId = user.DealerId,
            Email = user.Email,
            UserName = user.UserName,
            ImageUrl = user.Image.Url,
            Dealer = new DealerServiceModel()
            {
                DealerNumber = user.Dealer.DealerNumber,
                Name = user.Dealer.Name,
                PhoneNumber = user.Dealer.PhoneNumber,
                SiteUrl = user.Dealer.SiteUrl ?? string.Empty,
                Address = new AddressServiceModel()
                {
                    StreetName = user.Dealer.Address.StreetName,
                    City = new CityServiceModel()
                    {
                        Id = user.Dealer.Address.CityId,
                        Name = user.Dealer.Address.City.Name,
                        ZipCode = user.Dealer.Address.City.ZipCode
                    }
                }
            }
        };

        return model;
    }

    public async Task<OperationResult<DealerResultServiceModel>> Edit(string dealerId, EditDealerServiceModel serviceModel)
    {
        var mailExists = await this.data.Users
            .AnyAsync(x => x.Email == serviceModel.Email);
        
        if (mailExists)
        {
            return EmailExistsMsg;
        }
        
        var city = await this.data.Cities
            .FirstOrDefaultAsync(x => x.Name == serviceModel.CityName);

        if (city is null)
        {
            return InvalidCityMsg;
        }
        
        var user = await this.data.Dealers
            .Include(x => x.Address)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == dealerId);

        if (user is null)
        {
            return UserNotDealerMsg;
        }

        user.Email = serviceModel.Email;
        user.User.UserName = serviceModel.Email;
        user.User.Email = serviceModel.Email;
        user.Address.StreetName = serviceModel.StreetName;
        user.Address.CityId = city.Id;
        user.Name = serviceModel.Name;
        user.SiteUrl = serviceModel.SiteUrl;
        user.DealerNumber = serviceModel.DealerNumber;
        user.PhoneNumber = serviceModel.Phone;

        await this.data.SaveChangesAsync();

        return new DealerResultServiceModel()
        {
            DealerId = user.Id,
            DealerUserName = user.User.UserName,
            UserId = user.UserId
        };
    }
}