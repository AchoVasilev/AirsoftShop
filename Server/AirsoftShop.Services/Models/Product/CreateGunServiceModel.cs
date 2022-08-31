namespace AirsoftShop.Services.Models.Product;

using Base;

public class CreateGunServiceModel : ExtendedBaseGunServiceModel
{
        public IEnumerable<string> ImageIds { get; init; }
        
        public string SubCategoryName { get; init; }
}