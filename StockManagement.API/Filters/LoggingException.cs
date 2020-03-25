using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.API.Filters
{
    public class LoggingException : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;
        public LoggingException(ILoggerFactory loggerFactory) => _logger = loggerFactory.CreateLogger<LoggingException>();
        public override void OnException(ExceptionContext context) => _logger.LogError($"Exception : {context.Exception.ToString()}");
    }
}
