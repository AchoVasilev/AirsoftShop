namespace AirsoftShop.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Services.City;

public class CitiesController : BaseController
{
    private readonly ICityService cityService;

    public CitiesController(ICityService cityService) 
        => this.cityService = cityService;

    [HttpGet]
    public async Task<ActionResult> GetAll()
        => this.Ok(await this.cityService.GetAll());
}