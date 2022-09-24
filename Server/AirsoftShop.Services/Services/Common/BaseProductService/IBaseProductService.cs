namespace AirsoftShop.Services.Services.Common.BaseProductService;

using System.Linq.Expressions;
using AirsoftShop.Common.Models;

public interface IBaseProductService<TEntity, TResult> : ITransientService
    where TEntity : class
    where TResult : class
{
    Task<OperationResult<TResult>> Create(IProduct model, string dealerId);

    Task<OperationResult<TResult>> Edit(IProduct product, string dealerId,
        Expression<Func<TEntity, bool>> filter);
}