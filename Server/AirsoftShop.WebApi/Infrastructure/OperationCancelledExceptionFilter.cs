namespace AirsoftShop.WebApi.Infrastructure;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class OperationCancelledExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is OperationCanceledException)
        {
            context.ExceptionHandled = true;
            context.Result = new StatusCodeResult((int)HttpStatusCode.BadRequest);
        }
    }
}