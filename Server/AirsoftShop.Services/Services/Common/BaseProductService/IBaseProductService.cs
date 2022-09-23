namespace AirsoftShop.Services.Services.Common.BaseProductService;

using AirsoftShop.Common.Models;

public interface IBaseProductService <TEntity, TResult> : ITransientService
    where TEntity: class
    where TResult : class
{
    Task<OperationResult<TResult>> Create(IProduct model, string dealerId);
}