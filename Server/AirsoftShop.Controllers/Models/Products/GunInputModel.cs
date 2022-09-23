namespace AirsoftShop.Controllers.Models.Products
{
    using Base;
    using Microsoft.AspNetCore.Http;

    public class GunInputModel : BaseGunModel
    {
        public GunInputModel()
        {
            this.Images = new List<IFormFile>();
        }
        
        public int SubCategoryId { get; set; }
        
        public List<IFormFile> Images { get; }
    }
}