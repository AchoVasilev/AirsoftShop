namespace AirsoftShop.Services.Services.Product.Clothing;

using AirsoftShop.Common.Models;
using Data.Models.Images;
using Data.Models.Products;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Models.Product.Clothings;
using static AirsoftShop.Common.Constants.Messages;

public class ClothingService : IClothingService
{
    private readonly ApplicationDbContext data;

    public ClothingService(ApplicationDbContext data) 
        => this.data = data;

    public async Task<OperationResult<ClothingResultServiceModel>> Create(CreateProductServiceModel serviceModel, string userDealerId)
    {
        var dealer = await this.data.Dealers
            .FirstOrDefaultAsync(x => x.Id == userDealerId);

        if (dealer is null)
        {
            return NotAuthorizedMsg;
        }

        var categoryExists = await this.data.SubCategories
            .AnyAsync(x => x.Id == serviceModel.SubcategoryId);
        if (!categoryExists)
        {
            return InvalidSubcategoryErrorMsg;
        }

        var clothing = new Clothing
        {
            Name = serviceModel.Name,
            Manufacturer = serviceModel.Manufacturer,
            Price = serviceModel.Price,
            DealerId = dealer.Id,
            SubCategoryId = serviceModel.SubcategoryId,
            Size = serviceModel.Size,
            Material = serviceModel.Material,
            Description = serviceModel.Description,
            Color = serviceModel.Color,
            Images = serviceModel.Images.Select(x => new ItemImage()
            {
                Url = x.Uri,
                Name = x.Name,
                Extension = x.Extension
            }).ToList()
        };

        dealer.Clothings.Add(clothing);
        await this.data.SaveChangesAsync();

        var result = new ClothingResultServiceModel()
        {
            Id = clothing.Id,
            Name = clothing.Name
        };

        return result;
    }
}