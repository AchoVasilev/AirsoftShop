namespace AirsoftShop.Services.Services.File;

using CloudinaryDotNet;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Models.Files;

public interface IFileService
{
    Task<OperationResult<IFileServiceModel>> UploadImage(Cloudinary cloudinary, IFormFile? image, string folderName);

    Task<string> AddImageToDatabase(IFileServiceModel model);
}