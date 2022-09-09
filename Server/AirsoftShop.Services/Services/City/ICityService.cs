namespace AirsoftShop.Services.Services.City;

using Common;
using Models.City;

public interface ICityService : ITransientService
{
    Task<IEnumerable<CityServiceModel>> GetAll();
}