namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Images;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    [ForeignKey(nameof(Client))]
    public string ClientId { get; set; }

    public virtual Client Client { get; set; }

    [ForeignKey(nameof(Dealer))]
    public string DealerId { get; set; }

    public virtual Dealer Dealer { get; set; }

    [ForeignKey(nameof(Image))]
    public string ImageId { get; set; }

    public virtual Image Image { get; set; }
}