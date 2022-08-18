namespace AirsoftShop.Data.Models.Base;

public class Entity<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public string? CreatedBy { get; set; }
    
    public DateTime? ModifiedOn { get; set; }
    
    public string? ModifiedBy { get; set; }
}