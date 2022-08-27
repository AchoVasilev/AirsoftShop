namespace AirsoftShop.Services.Models.File
{
    public interface IFileServiceModel
    {
        string Extension { get; set; }

        string Uri { get; set; }

        string Name { get; set; }
    }
}
