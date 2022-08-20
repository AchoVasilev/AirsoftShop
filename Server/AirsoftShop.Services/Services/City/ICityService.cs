namespace AirsoftShop.Services.Services.City;

using Models.Cities;

public interface ICityService
{
    Task<IEnumerable<BaseCityServiceModel>> GetAll();
}