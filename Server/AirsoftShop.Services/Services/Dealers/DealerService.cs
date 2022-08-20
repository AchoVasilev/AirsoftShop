namespace AirsoftShop.Services.Services.Dealers;

using Common.Models;
using Data.Models;
using Data.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Dealers;
using static Common.Constants.Messages;

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
}