namespace AirsoftShop.Services.Services.City;

using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.City;

public class CityService : ICityService
{
    private readonly ApplicationDbContext data;

    public CityService(ApplicationDbContext data) 
        => this.data = data;

    public async Task<IEnumerable<CityServiceModel>> GetAll()
        => await this.data.Cities
            .Select(x => new CityServiceModel()
            {
                Id = x.Id,
                Name = x.Name,
                ZipCode = x.ZipCode
            })
            .ToListAsync();
}