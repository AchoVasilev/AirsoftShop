namespace AirsoftShop.Controllers;

using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return null;
    }

    public IActionResult Privacy()
    {
        return null;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return null;
    }
}
