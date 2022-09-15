namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base;
using Products;
using static Constants.Data.Constants;

public class Dealer : DeletableEntity<string>
{
    public Dealer()
    {
        this.Id = Guid.NewGuid().ToString();
        this.Orders = new HashSet<Order>();
        this.Guns = new HashSet<Gun>();
        this.Fields = new HashSet<Field>();
        this.Clothings = new HashSet<Clothing>();
    }

    [Required]
    [MaxLength(DefaultMaxLength)]
    public string Name { get; set; }

    [Required]
    [MaxLength(DefaultMaxLength)]
    public string DealerNumber { get; set; }

    [Required] 
    public string PhoneNumber { get; set; }

    public string SiteUrl { get; set; }

    [Required] 
    public string Email { get; set; }

    [ForeignKey(nameof(Address))] 
    public int AddressId { get; set; }

    public virtual Address Address { get; init; }

    [ForeignKey(nameof(User))] 
    public string UserId { get; set; }

    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<Order> Orders { get; set; }

    public virtual ICollection<Gun> Guns { get; set; }
    
    public virtual ICollection<Clothing> Clothings { get; set; }

    public virtual ICollection<Field> Fields { get; set; }
}