using System.Net;

namespace RedditSharp.API.MiddleWare
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;
        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
        {
            _logger = logger;   
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex) 
            { 
                var response = httpContext.Response;
                response.ContentType = "application/json";

                switch(ex)
                {
                    case KeyNotFoundException:
                        {
                            response.StatusCode = (int)HttpStatusCode.NotFound; 
                            break;
                        }
                    default:
                        {
                            response.StatusCode = (int)HttpStatusCode.InternalServerError; 
                            break;
                        }
                }
                _logger.LogError(ex, ex?.Message ?? string.Empty);
                var result = System.Text.Json.JsonSerializer.Serialize(new { message = ex?.Message });
                await response.WriteAsync(result);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandler>();
        }
    }
}
