namespace AirsoftShop.Services.Common;

using AirsoftShop.Common.Models;
using Data.Persistence;
using Microsoft.EntityFrameworkCore;

public abstract class BaseProductService<T, R> : IBaseProductService<T, R>
    where T : class
    where R : class
{
    public BaseProductService(ApplicationDbContext data)
    {
        this.Context = data;
    }

    protected DbSet<T> DbSet => this.Context.Set<T>();

    protected ApplicationDbContext Context { get; }

    public abstract Task<OperationResult<R>> Create(IProduct model, string dealerId);
}