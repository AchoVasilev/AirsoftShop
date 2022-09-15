namespace AirsoftShop.Controllers.Models.Products.Guns
{
    using AirsoftShop.Controllers.Models.Products.Base;
    using Microsoft.AspNetCore.Http;

    public class GunInputModel : BaseGunModel
    {
        public GunInputModel()
        {
            this.Images = new List<IFormFile>();
        }
        
        public List<IFormFile> Images { get; }
    }
}