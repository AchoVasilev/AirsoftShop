namespace AirsoftShop.Common.Models;

public class OperationResult
{
    public string? ErrorMessage { get; private init; }
    
    public List<string>? ErrorMessages { get; private init; }

    public bool Succeeded { get; private init; }

    public bool Failed => this.Succeeded == false;
    
    public static implicit operator OperationResult(bool succeeded)
        => new OperationResult()
        {
            Succeeded = succeeded
        };
    
    public static implicit operator OperationResult(string errorMessage)
        => new OperationResult()
        {
            Succeeded = false,
            ErrorMessage = errorMessage
        };

    public static implicit operator OperationResult(List<string> errorMessages)
        => new OperationResult()
        {
            Succeeded = false,
            ErrorMessages = errorMessages
        };
}

public class OperationResult<T> where T: class
{
    public string? ErrorMessage { get; private init; }
    
    public List<string>? ErrorMessages { get; private init; }

    public bool Succeeded { get; private init; }

    public bool Failed => this.Succeeded == false;
    
    public T? Model { get; private init; }
    
    public IEnumerable<T>? Models { get; private init; }

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