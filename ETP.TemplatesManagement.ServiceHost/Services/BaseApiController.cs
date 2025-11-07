using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ETP.TemplatesManagement.ServiceHost.Services
{
    public class BaseApiController : ControllerBase
    {
        protected readonly ILogger logger;

        public BaseApiController(ILogger logger)
        {
            this.logger = logger;
        }

        protected async Task<IActionResult> ExecuteApiCall(Func<Task<IActionResult>> action, string errorMessage)
        {
            try
            {
                return await action();
            }
            catch (ValidationException ex)
            {
                logger.LogError(ex, errorMessage);
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                logger.LogError(ex, errorMessage);
                return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
            }
            catch (MongoDB.Driver.MongoWriteException ex)
            {
                logger.LogError(ex, errorMessage);
                if (ex.WriteError.Category == MongoDB.Driver.ServerErrorCategory.DuplicateKey)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ex.WriteError.Message);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex.InnerException, errorMessage);
                return ex.StatusCode == null
                     ? StatusCode(StatusCodes.Status500InternalServerError, errorMessage)
                     : StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                logger.LogError(ex, errorMessage);
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (ApplicationException ex)
            {
                logger.LogError(ex, errorMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex) when (ex is OperationCanceledException
                                   || ex is TaskCanceledException)
            {
                logger.LogError(ex, errorMessage);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, errorMessage);
                return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
            }
        }
    }
}
