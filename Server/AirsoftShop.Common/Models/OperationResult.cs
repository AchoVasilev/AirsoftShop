namespace AirsoftShop.Common.Models;

public class OperationResult<T> 
    where T : class
{
    public string? ErrorMessage { get; set; }
    
    public bool Succeeded { get; private set; }
    
    public string? SuccessMessage { get; set; }

    public bool Failed => this.Succeeded == false;
    
    public T? Model { get; set; }
    
    public static implicit operator OperationResult<T> (bool succeeded)
        => new OperationResult<T> () { Succeeded = succeeded };
    
    public static implicit operator OperationResult<T> (string errorMessage)
        => new OperationResult<T> ()
        {
            Succeeded = false,
            ErrorMessage = errorMessage
        };
}