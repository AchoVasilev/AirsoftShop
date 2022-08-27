namespace AirsoftShop.Services.Services.Dealer;

using AirsoftShop.Common.Models;
using Models.Dealer;

public interface IDealerService
{
    Task<OperationResult<DealerResultServiceModel>> CreateDealer(CreateDealerServiceModel model, string imageId);

    Task<OperationResult<UserDealerServiceModel>> Profile(string userId);
}