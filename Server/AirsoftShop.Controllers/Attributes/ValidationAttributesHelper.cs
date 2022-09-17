namespace AirsoftShop.Controllers.Attributes;

using Microsoft.AspNetCore.Mvc.Filters;

internal static class ValidationAttributesHelper
{
    internal static T? RequestServiceContext<T>(ActionExecutingContext context) 
        => (T)context.HttpContext.RequestServices.GetService(typeof(T))!;
}