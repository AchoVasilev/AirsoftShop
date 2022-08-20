namespace AirsoftShop.Services.Services.Dealers;

using Common.Models;
using Models.Dealers;

public interface IDealerService
{
    Task<OperationResult<DealerResultServiceModel>> CreateDealer(CreateDealerServiceModel model, string imageId);

    Task<OperationResult<UserDealerServiceModel>> Profile(string userId);
}