namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base;
using Enums;

public class Order : DeletableEntity<string>
{
    public Order()
    {
        this.Id = Guid.NewGuid().ToString();
        this.Dealers = new HashSet<Dealer>();
        this.Guns = new HashSet<Gun>();
    }

    public PaymentType PaymentType { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public decimal TotalPrice { get; set; }

    [ForeignKey(nameof(Courier))]
    public int? CourierId { get; set; }

    public virtual Courier Courier { get; set; }

    [ForeignKey(nameof(Client))]
    [Required]
    public string ClientId { get; set; }

    public virtual Client Client { get; set; }

    public virtual ICollection<Gun> Guns { get; set; }

    public virtual ICollection<Dealer> Dealers { get; set; }
}