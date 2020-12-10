using CopBookApi.Interfaces.Services.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace CopBookApi.Filters.Logging
{
    public class LoggingActionFilter : IActionFilter
    {
        private readonly ILoggingService logger;

        public LoggingActionFilter(ILoggingService logger)
        {
            this.logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                logger.Log(LogLevel.Error, $"ERROR IN CALL: {BuildLogMessage(context)}", context.Exception);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.Log(LogLevel.Debug, BuildLogMessage(context));
        }

        private string BuildLogMessage(FilterContext context)
        {
            string verb = context.HttpContext.Request.Method;
            string endpoint = context.HttpContext.Request.Path;
            string userId = context.HttpContext.User?.FindFirst("user_id")?.Value;
            string formattedUserId = userId != null ? $" - {userId}" : "";
            return $"{verb} {endpoint}{formattedUserId}";
        }
    }
}