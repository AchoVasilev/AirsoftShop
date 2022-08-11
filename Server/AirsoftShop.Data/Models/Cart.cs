namespace AirsoftShop.Data.Models;

using System.ComponentModel.DataAnnotations.Schema;
using Base;

public class Cart : DeletableEntity<string>
{
    public Cart()
    {
        this.Id = Guid.NewGuid().ToString();
        this.Guns = new HashSet<Gun>();
    }

    [ForeignKey(nameof(Client))]
    public string ClientId { get; set; }

    public virtual Client Client { get; set; }

    public virtual ICollection<Gun> Guns { get; set; }
}