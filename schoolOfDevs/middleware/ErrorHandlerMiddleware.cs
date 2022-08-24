using System.Net;
using System.Text.Json;
using schoolOfDevs.Exceptions;

namespace schoolOfDevs.middleware
{
    // responsavel por capturar as exceção
    //para nao usar try catch em tudo
    public class ErrorHandlerMiddleware
    {

        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {

                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    BadRequestException => (int)HttpStatusCode.BadRequest, // requisição mal feita
                    KeyNotFoundException => (int)HttpStatusCode.BadRequest, //nao encontrado
                    ForbiddenException => (int)HttpStatusCode.Forbidden, //não autorizado
                    _ => (int)HttpStatusCode.InternalServerError,
                };

                var result = JsonSerializer.Serialize(new {message = error?.Message});
                await response.WriteAsync(result);
            }
        }
    }
}
