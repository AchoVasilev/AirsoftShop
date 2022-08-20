namespace AirsoftShop.Services.Models.Files
{
    public interface IFileServiceModel
    {
        string Extension { get; set; }

        string Uri { get; set; }

        string Name { get; set; }
    }
}
