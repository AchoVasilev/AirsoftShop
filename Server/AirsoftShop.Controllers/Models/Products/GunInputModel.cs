namespace AirsoftShop.Controllers.Models.Products
{
    using Base;
    using Microsoft.AspNetCore.Http;

    public class GunInputModel : BaseGunModel
    {
        public List<IFormFile> Images { get; set; }
    }
}