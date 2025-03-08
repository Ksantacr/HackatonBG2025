using bg.hackathon.alphahackers.domain.models;
using System.Net;
using System.Text.Json;

namespace bg.hackathon.alphahackers.api.middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
                _logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(context, ex); //ejemplo
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400

            var traceId = context.TraceIdentifier; // Obtiene el TraceId generado por ASP.NET Core

            // Crear la respuesta usando la clase del dominio
            var response = new ErrorResponse
            {
                Code = context.Response.StatusCode,
                TraceId = traceId,
                Message = "An error occurred while processing your request.",
                Errors = new List<ErrorDetail>
                {
                    new ErrorDetail
                    {
                        Code = 1001, // Código de error personalizado
                        Message = exception.Message // Mensaje de la excepción
                    }
                }
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
