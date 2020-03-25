using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.API.Filters
{
    public class LoggingAction : ActionFilterAttribute
    {
        private readonly ILogger _logger;
        public LoggingAction(ILoggerFactory loggerFactory) => _logger = loggerFactory.CreateLogger<LoggingAction>();
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = (context.ActionDescriptor as ControllerActionDescriptor);
            _logger.LogInformation("{ControllerName} {ActionName} {QueryString}", controller.ControllerName, controller.ActionName, context.HttpContext.Request.QueryString.ToString());
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
