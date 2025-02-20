using Microsoft.AspNetCore.Mvc;

namespace VAT_AT_Calc.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception)
            {
                var traceId = Guid.NewGuid(); // this should be logged

                httpContext.Response.ContentType = "application/json";
                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = "Internal Server Error",
                    Status = (int)StatusCodes.Status500InternalServerError,
                    Instance = httpContext.Request.Path,
                    Detail = $"Internal server error occured, traceId : {traceId}",
                };
                await httpContext.Response.WriteAsJsonAsync(problemDetails);
            }

        }
    }
}
