namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base;
using Images;
using static Constants.Data.Constants;
public class Courier : DeletableEntity<int>
{
    public Courier()
    {
        this.Orders = new HashSet<Order>();
    }

    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Name { get; init; }

    public decimal DeliveryPrice { get; init; }

    public int DeliveryDays { get; init; }

    [ForeignKey(nameof(Image))]
    public string ImageId { get; set; }

    public virtual Image Image { get; init; }

    public virtual ICollection<Order> Orders { get; set; }
}