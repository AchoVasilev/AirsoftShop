namespace AirsoftShop.Services.Common;

using AirsoftShop.Common.Models;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

public abstract class BaseProductService<T, R, E> : IBaseProductService<T, R, E>
    where T : class
    where R : class
    where E : class
{
    public BaseProductService(ApplicationDbContext data)
    {
        this.Context = data;
    }

    protected DbSet<T> DbSet => this.Context.Set<T>();

    protected ApplicationDbContext Context { get; }

    public abstract Task<OperationResult<R>> CreateGun(E model, string dealerId);
}