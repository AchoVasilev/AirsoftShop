namespace AirsoftShop.Services.Common;

using AirsoftShop.Common.Models;

public interface IBaseProductService <T, R, E> where R : class
{
    Task<OperationResult<R>> CreateGun(E model, string dealerId);

}