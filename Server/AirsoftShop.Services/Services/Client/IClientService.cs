namespace AirsoftShop.Services.Services.Client;

using Common.Models;
using Models.Client;

public interface IClientService
{
    Task<OperationResult<ClientResultServiceModel>> CreateClient(CreateClientServiceModel model);

    Task<OperationResult<UserClientServiceModel>> Profile(string userId);
    
    Task<OperationResult<ClientResultServiceModel>> EditClient(string? userId, ClientEditServiceModel editModel);
}