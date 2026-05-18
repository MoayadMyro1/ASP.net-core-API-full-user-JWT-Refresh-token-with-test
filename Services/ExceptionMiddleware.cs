using DriverApi.Services;
using System.Net;
using System.Text.Json;

namespace DriverApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";

                context.Response.StatusCode =
                    (int)HttpStatusCode.InternalServerError;
                var errors = new List<string>
                {
                    ex.Message
                };

                if (ex.InnerException != null)
                {
                    errors.Add(ex.InnerException.Message);
                }

                var response = new ErrorRespone<string>
                {
                    Success = false,
                    Message = "Internal Server Error",
                    Errors = errors
                };

                var json =
                    JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}