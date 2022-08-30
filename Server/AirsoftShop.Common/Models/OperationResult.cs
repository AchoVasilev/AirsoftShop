namespace AirsoftShop.Common.Models;

public class OperationResult<T>
{
    public string? ErrorMessage { get; init; }
    
    public List<string>? ErrorMessages { get; init; }

    public bool Succeeded { get; private set; }

    public bool Failed => this.Succeeded == false;
    
    public T? Model { get; init; }
    
    public IEnumerable<T>? Models { get; init; }

    public static implicit operator OperationResult<T>(bool succeeded)
        => new OperationResult<T>()
        {
            Succeeded = succeeded
        };

    public static implicit operator OperationResult<T> (T model)
        => new OperationResult<T> ()
        {
            Model = model,
            Succeeded = true
        };

    public static implicit operator OperationResult<T>(List<T> models)
        => new OperationResult<T>()
        {
            Models = models,
            Succeeded = true
        };

    public static implicit operator OperationResult<T> (string errorMessage)
        => new OperationResult<T> ()
        {
            Succeeded = false,
            ErrorMessage = errorMessage
        };

    public static implicit operator OperationResult<T>(List<string> errorMessages)
        => new OperationResult<T>()
        {
            Succeeded = false,
            ErrorMessages = errorMessages
        };
}