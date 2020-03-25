using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.API.Filters
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate requestNext;
        private readonly ILogger loggerFactory;
        public LoggingMiddleware(RequestDelegate _requestNext, ILoggerFactory _loggerFactory)
        {
            requestNext = _requestNext;
            loggerFactory = _loggerFactory.CreateLogger<LoggingMiddleware>();
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestNext(context);
            }
            finally
            {
                loggerFactory.LogInformation("Details of API Request {method} {url} {query}=> {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Request?.QueryString.ToString(),
                    context.Response?.StatusCode);
            }
        }
    }
}
