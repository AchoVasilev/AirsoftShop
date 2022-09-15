namespace AirsoftShop.Services.Services.Product.Clothing;

using AirsoftShop.Common.Models;
using Models.Product.Clothings;

public class ClothingService : IClothingService
{
    public Task<OperationResult<ClothingResultServiceModel>> Create(CreateProductServiceModel serviceModel, string userDealerId)
    {
        throw new NotImplementedException();
    }
}