namespace AirsoftShop.Services.Models.Product.Guns;

using AirsoftShop.Services.Models.File;
using AirsoftShop.Services.Models.Product.Base;

public class CreateGunServiceModel : ExtendedBaseGunServiceModel
{
        public IEnumerable<IFileServiceModel>? Images { get; init; }
}