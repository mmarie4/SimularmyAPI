using Domain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace SimularmyAPI.Middleware
{

    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var statusCode = 500;
            if (context.Exception is DomainException domainException)
            {
                statusCode = domainException.Code;
            }
            if (context.Exception is ValidationException)
            {
                statusCode = 400;
            }

            var errorMessage = context.Exception.InnerException == null
                               ? context.Exception.Message
                               : $"{context.Exception.Message} - {context.Exception.InnerException.Message}";


            context.Result = new ObjectResult(new ErrorResponse(errorMessage))
            {
                StatusCode = statusCode
            };
        }
    }
}
