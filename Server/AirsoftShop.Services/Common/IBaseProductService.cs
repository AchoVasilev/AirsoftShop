namespace AirsoftShop.Services.Common;

using AirsoftShop.Common.Models;

public interface IBaseProductService <T, R> 
    where R : class
{
    Task<OperationResult<R>> Create(IProduct model, string dealerId);
}