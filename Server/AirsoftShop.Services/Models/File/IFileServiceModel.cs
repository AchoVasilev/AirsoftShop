namespace AirsoftShop.Services.Models.File
{
    public interface IFileServiceModel
    {
        string? Extension { get; }

        string? Uri { get; }

        string? Name { get; }
    }
}
