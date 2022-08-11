namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Base;
using Images;

public class Field : DeletableEntity<int>
{
    [ForeignKey(nameof(Dealer))]
    public string DealerId { get; set; }

    public virtual Dealer Dealer { get; set; }

    [ForeignKey(nameof(Address))]
    public int AddressId { get; set; }

    public virtual Address Address { get; set; }

    [ForeignKey(nameof(Image))]
    public string ImageId { get; set; }

    public virtual Image Image { get; set; }
}