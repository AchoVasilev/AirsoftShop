namespace AirsoftShop.Services.Models.Product.Guns;

using File;
using Base;

public class CreateGunServiceModel : ExtendedBaseGunServiceModel
{
        public IEnumerable<IFileServiceModel>? Images { get; init; }
}