namespace AirsoftShop.Data.Models.Base;

public class DeletableEntity<TKey> : Entity<TKey>, IDeletableEntity<TKey>
{
    public DateTime? DeletedOn { get; set; }
    
    public string DeletedBy { get; set; }
    
    public bool IsDeleted { get; set; }
}