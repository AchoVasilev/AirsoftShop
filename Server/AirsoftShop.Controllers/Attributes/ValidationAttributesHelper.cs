namespace AirsoftShop.Controllers.Attributes;

using Microsoft.AspNetCore.Mvc.Filters;

public static class ValidationAttributesHelper
{
    public static T? RequestServiceContext<T>(ActionExecutingContext context) 
        => (T)context.HttpContext.RequestServices.GetService(typeof(T))!;
}