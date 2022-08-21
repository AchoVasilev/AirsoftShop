namespace AirsoftShop.Services.Services.Client;

using Common.Models;
using Models.Clients;

public interface IClientService
{
    Task<OperationResult<ClientResultServiceModel>> CreateClient(CreateClientServiceModel model);

    Task<OperationResult<UserClientServiceModel>> Profile(string userId);
}