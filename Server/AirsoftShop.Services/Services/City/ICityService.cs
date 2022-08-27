namespace AirsoftShop.Services.Services.City;

using Models.City;

public interface ICityService
{
    Task<IEnumerable<BaseCityServiceModel>> GetAll();
}