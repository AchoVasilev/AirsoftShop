namespace AirsoftShop.Services.Services.File;

using Common.Models;
using Microsoft.AspNetCore.Http;
using Models.File;

public interface IFileService
{
    Task<OperationResult<IFileServiceModel>> UploadImage(IFormFile? image, string folderName);

    Task<string> AddImageToDatabase(IFileServiceModel model);
}