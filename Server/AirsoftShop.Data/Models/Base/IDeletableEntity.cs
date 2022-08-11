namespace AirsoftShop.Data.Models.Base;

public interface IDeletableEntity<TKey> : IEntity<TKey>
{
    DateTime? DeletedOn { get; set; }
    
    string DeletedBy { get; set; }
    
    bool IsDeleted { get; set; }
}