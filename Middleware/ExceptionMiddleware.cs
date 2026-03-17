using System.Text.Json;
using PortfolioApi.Shared.Exceptions;
using PortfolioApi.Shared.Responses;

namespace PortfolioApi.Middleware;

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
        catch (StockNotFoundException ex)
        {
            await HandleException(context, ex, 404, "stock not found");

        }
        catch (Exception ex)
        {
            await HandleException(context, ex, 404, "InternalServerError");

        }

    }

    private async Task HandleException(HttpContext context, Exception ex, int statusCode, string error)
    {
              
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new ErrorResponse
        {
            Error = error,
            Message = ex.Message,
            StatusCode= statusCode
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}
