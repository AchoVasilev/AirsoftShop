namespace AirsoftShop.Services.Services.File
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Common.Models;
    using Data.Models.Images;
    using Data.Persistence;
    using Microsoft.AspNetCore.Http;
    using Models.File;
    using static Common.Constants.Messages;
    public class FileService : IFileService
    {
        private readonly ApplicationDbContext data;

        public FileService(ApplicationDbContext data) 
            => this.data = data;

        public async Task<OperationResult<IFileServiceModel>> UploadImage(Cloudinary cloudinary, IFormFile? image, string folderName)
        {
            if (image is null)
            {
                return InvalidImage;
            }

            var allowedExtensions = new[] { "jpg", "png", "gif", "jpeg" };

            var extension = Path.GetExtension(image.FileName).TrimStart('.');

            if (!allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                return InvalidImageExtension;
            }

            var imageName = image.FileName;

            byte[] destinationImage;
            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            var imageModel = new ImageServiceModel();
            using (var ms = new MemoryStream(destinationImage))
            {
                // Cloudinary doesn't work with [?, &, #, \, %, <, >]
                imageName = imageName.Replace("&", "And");
                imageName = imageName.Replace("#", "sharp");
                imageName = imageName.Replace("?", "questionMark");
                imageName = imageName.Replace("\\", "right");
                imageName = imageName.Replace("%", "percent");
                imageName = imageName.Replace(">", "greater");
                imageName = imageName.Replace("<", "lower");

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imageName, ms),
                    PublicId = $"{folderName}/{imageName}",
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                if (uploadResult is null)
                {
                    return UnsuccessfulActionMsg;
                }
                
                imageModel.Extension = extension;
                imageModel.Uri = uploadResult.SecureUrl.AbsoluteUri;
                imageModel.Name = imageName;
            }

            return imageModel;
        }

        public async Task<string> AddImageToDatabase(IFileServiceModel model)
        {
            var image = new Image
            {
                Extension = model.Extension,
                Url = model.Uri,
                Name = model.Name
            };

            await this.data.Images.AddAsync(image);
            await this.data.SaveChangesAsync();

            return image.Id;
        }
    }
}