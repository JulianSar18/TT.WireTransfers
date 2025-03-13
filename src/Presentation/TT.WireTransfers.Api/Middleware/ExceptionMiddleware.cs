using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TT.WireTransfers.Domain.Exceptions;

namespace TT.WireTransfers.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (WalletNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); // Para debug
                context.Response.StatusCode = 500;
                var errorResponse = new Dictionary<string, string> { { "error", "Internal server error" } };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
