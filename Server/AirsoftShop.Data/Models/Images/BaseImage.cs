namespace AirsoftShop.Data.Models.Images;

using Base;

public abstract class BaseImage : DeletableEntity<string>
{
    public BaseImage()
    {
        this.Id = Guid.NewGuid().ToString();
    }
    
    public string RemoteImageUrl { get; set; }

    public string Url { get; init; }

    public string Name { get; set; }

    public string Extension { get; set; }
}