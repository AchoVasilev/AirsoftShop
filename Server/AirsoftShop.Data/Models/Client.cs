namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base;
using static Constants.Data.Constants;

public class Client : DeletableEntity<string>
{
    public Client()
    {
        this.Id = Guid.NewGuid().ToString();
        this.Orders = new HashSet<Order>();
    }

    [Required]
    [MaxLength(DefaultMaxLength)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(DefaultMaxLength)]
    public string LastName { get; set; }

    [Required] 
    public string PhoneNumber { get; set; }

    [Required] 
    public string Email { get; set; }

    [ForeignKey(nameof(Address))]
    public int AddressId { get; set; }

    public virtual Address Address { get; init; }

    [ForeignKey(nameof(User))] 
    public string UserId { get; set; }

    public virtual ApplicationUser User { get; set; }

    [ForeignKey(nameof(Cart))] 
    public string CartId { get; set; }

    public virtual Cart Cart { get; set; }

    [ForeignKey(nameof(WishList))] 
    public string WishListId { get; set; }

    public virtual WishList WishList { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}