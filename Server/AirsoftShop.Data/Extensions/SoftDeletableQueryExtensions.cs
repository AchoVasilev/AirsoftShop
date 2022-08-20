namespace AirsoftShop.Data.Extensions;

using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using Models.Base;

public static class SoftDeletableQueryExtensions
{
    public static void AddSoftDeletableQueryFilter<TKey>(this IMutableEntityType entity)
    {
        var methodToCall = typeof(SoftDeletableQueryExtensions)
            .GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
            .MakeGenericMethod(entity.ClrType, typeof(TKey));

        var filter = methodToCall.Invoke(null, new object[] { });
        entity.SetQueryFilter((LambdaExpression)filter);
        entity.AddIndex(entity.FindProperty(nameof(IDeletableEntity<TKey>.IsDeleted)));
    }

    private static LambdaExpression GetSoftDeleteFilter<TEntity, TKey>()
        where TEntity : class, IDeletableEntity<TKey>
    {
        Expression<Func<TEntity, bool>> filter = x => x.IsDeleted == false;

        return filter;
    }
}