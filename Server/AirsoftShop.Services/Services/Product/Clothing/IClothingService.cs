namespace AirsoftShop.Services.Services.Product.Clothing;

using AirsoftShop.Common.Models;
using Models.Product.Clothings;

public interface IClothingService
{
    Task<OperationResult<ClothingResultServiceModel>> Create(CreateProductServiceModel serviceModel, string userDealerId);
}