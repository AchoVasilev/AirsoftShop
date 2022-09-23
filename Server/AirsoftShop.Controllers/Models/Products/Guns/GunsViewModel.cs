namespace AirsoftShop.Controllers.Models.Products.Guns;

using AirsoftShop.Common.Models;
using Services.Models.Product.Guns;

public class GunsViewModel : PagingModel
{
    public GunsViewModel()
    {
        this.AllGuns = new List<GunViewServiceModel>();
        this.Colors = new List<string>();
        this.Dealers = new List<string>();
        this.Manufacturers = new List<string>();
        this.Powers = new List<double>();
    }
    
    public ICollection<GunViewServiceModel> AllGuns { get; set; }

    public ICollection<string> Colors { get; set; }

    public ICollection<string> Dealers { get; set; }

    public ICollection<string> Manufacturers { get; set; }

    public ICollection<double> Powers { get; set; }
}