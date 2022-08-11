namespace AirsoftShop.Data.Models.Base;

public interface IEntity<TKey>
{ 
    TKey Id { get; set; }
    
    DateTime CreatedOn { get; set; }
    
    string CreatedBy { get; set; }
    
    DateTime? ModifiedOn { get; set; }
    
    string ModifiedBy { get; set; }
}