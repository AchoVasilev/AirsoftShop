namespace AirsoftShop.Services.Services.City;

using Common.Services.Common;
using Models.City;

public interface ICityService : ITransientService
{
    Task<IEnumerable<BaseCityServiceModel>> GetAll();
}