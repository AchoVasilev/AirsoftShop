using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AirsoftShop.WebApi.Controllers;

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
