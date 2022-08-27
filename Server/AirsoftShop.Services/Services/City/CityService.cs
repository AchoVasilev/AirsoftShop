namespace AirsoftShop.Services.Services.City;

using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.City;

public class CityService : ICityService
{
    private readonly ApplicationDbContext data;

    public CityService(ApplicationDbContext data) 
        => this.data = data;

    public async Task<IEnumerable<BaseCityServiceModel>> GetAll()
        => await this.data.Cities
            .Select(x => new BaseCityServiceModel()
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
}