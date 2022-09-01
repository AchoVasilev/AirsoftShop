namespace AirsoftShop.Services.Services.Dealer;

using AirsoftShop.Common.Models;
using Common.Services.Common;
using Models.Dealer;

public interface IDealerService : ITransientService
{
    Task<OperationResult<DealerResultServiceModel>> CreateDealer(CreateDealerServiceModel model, string imageId);

    Task<OperationResult<UserDealerServiceModel>> Profile(string userId);
    
    Task<OperationResult<DealerResultServiceModel>> Edit(string dealerId, EditDealerServiceModel serviceModel);
}