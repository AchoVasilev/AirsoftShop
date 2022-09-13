namespace AirsoftShop.Services.Models.Product;

using Base;
using Common;
using File;

public class CreateGunServiceModel : ExtendedBaseGunServiceModel
{
        public IEnumerable<IFileServiceModel>? Images { get; init; }
        
        public string? SubCategoryName { get; init; }
}