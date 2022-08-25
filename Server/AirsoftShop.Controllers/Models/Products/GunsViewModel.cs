namespace AirsoftShop.Controllers.Models.Products;

using Common.Models;
using Services.Models.Products;

public class GunsViewModel : PagingModel
{
    public ICollection<GunViewServiceModel> AllGuns { get; set; }

    public ICollection<string> Colors { get; set; }

    public ICollection<string> Dealers { get; set; }

    public ICollection<string> Manufacturers { get; set; }

    public ICollection<double> Powers { get; set; }
}