using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ETP.TemplatesManagement.ServiceHost.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            this.logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, $"Unhandled exception occurred: {context.ActionDescriptor.DisplayName}");

            var statusCode = HttpStatusCode.InternalServerError;

            switch (context.Exception)
            {
                case ValidationException:
                case ArgumentException:
                case InvalidOperationException:
                case InvalidDataException:
                    {
                        statusCode = HttpStatusCode.BadRequest;
                        break;
                    }
                case UnauthorizedAccessException:
                    {
                        statusCode = HttpStatusCode.Forbidden;
                        break;
                    }
                case KeyNotFoundException:
                    {
                        statusCode = HttpStatusCode.NotFound;
                        break;
                    }
                case MongoWriteException mongoWriteException:
                    {
                        statusCode = mongoWriteException.WriteError.Category == ServerErrorCategory.DuplicateKey
                            ? HttpStatusCode.BadRequest
                            : HttpStatusCode.InternalServerError;
                        break;
                    }
                case HttpRequestException httpRequestException:
                    {
                        statusCode = httpRequestException.StatusCode ?? HttpStatusCode.InternalServerError;
                        break;
                    }
                case TaskCanceledException:
                case OperationCanceledException:
                    {
                        statusCode = HttpStatusCode.ServiceUnavailable;
                        break;
                    }
                default:
                    {
                        statusCode = HttpStatusCode.InternalServerError;
                        break;
                    }
            }

            context.Result = new JsonResult(new
            {
                Error = context.Exception.Message,
                Details = context.Exception.InnerException?.Message
            })
            {
                StatusCode = (int)statusCode
            };
        }
    }
}
