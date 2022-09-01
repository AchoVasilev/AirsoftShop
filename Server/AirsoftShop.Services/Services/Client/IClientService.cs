namespace AirsoftShop.Services.Services.Client;

using AirsoftShop.Common.Models;
using Common;
using Models.Client;

public interface IClientService : ITransientService
{
    Task<OperationResult<ClientResultServiceModel>> CreateClient(CreateClientServiceModel model);

    Task<OperationResult<UserClientServiceModel>> Profile(string userId);
    
    Task<OperationResult<ClientResultServiceModel>> EditClient(string? userId, ClientEditServiceModel editModel);
}