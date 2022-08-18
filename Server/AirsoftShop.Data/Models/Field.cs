namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Base;
using Images;

public class Field : DeletableEntity<int>
{
    public Field()
    {
        this.Images = new HashSet<Image>();
    }
    
    [ForeignKey(nameof(Dealer))]
    public string DealerId { get; set; }

    public virtual Dealer Dealer { get; set; }

    [ForeignKey(nameof(Address))]
    public int AddressId { get; set; }

    public virtual Address Address { get; set; }

    public virtual IEnumerable<Image> Images { get; set; }
}