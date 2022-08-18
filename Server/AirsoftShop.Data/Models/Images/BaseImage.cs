namespace AirsoftShop.Data.Models.Images;

using Base;

public class BaseImage : DeletableEntity<string>
{
    public BaseImage()
    {
        this.Id = Guid.NewGuid().ToString();
    }
    
    public string? RemoteImageUrl { get; set; }

    public string? Url { get; set; }

    public string? Name { get; set; }

    public string? Extension { get; set; }
}