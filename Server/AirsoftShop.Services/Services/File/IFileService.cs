namespace AirsoftShop.Services.Services.File;

using AirsoftShop.Common.Models;
using Common;
using Microsoft.AspNetCore.Http;
using Models.File;

public interface IFileService : ITransientService
{
    Task<OperationResult<IFileServiceModel>> UploadImage(IFormFile? image, string folderName);

    Task<string> AddImageToDatabase(IFileServiceModel model);

    Task<string> AddItemImageToDatabase(IFileServiceModel model);
}