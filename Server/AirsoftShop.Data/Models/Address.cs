namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base;
using static Constants.Data.Constants;
public class Address : DeletableEntity<int>
{
    [Required]
    [MaxLength(DefaultMaxLength)]
    public string StreetName { get; set; }

    [ForeignKey(nameof(City))]
    public int CityId { get; set; }

    public virtual City City { get; set; }

    [ForeignKey(nameof(Client))]
    public string ClientId { get; set; }

    public virtual Client Client { get; set; }

    [ForeignKey(nameof(Dealer))]
    public string DealerId { get; set; }

    public virtual Dealer Dealer { get; set; }
}