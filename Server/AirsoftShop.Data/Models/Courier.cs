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
    public string Name { get; set; }

    public decimal DeliveryPrice { get; set; }

    public int DeliveryDays { get; set; }

    [ForeignKey(nameof(Image))]
    public string ImageId { get; set; }

    public virtual Image Image { get; set; }

    public ICollection<Order> Orders { get; set; }
}