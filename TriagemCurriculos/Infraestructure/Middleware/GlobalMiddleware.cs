


using System.Net;
using System.Text.Json.Serialization;
using TriagemCurriculos.Infraestructure;

namespace TriagemCurriculos.Infraestructure.Middleware
{
    public class GlobalMiddleware(MainDbContext db,
    HttpContextService httpContextService,
    IWebHostEnvironment env) : IMiddleware
    {
        static GlobalMiddleware()
        {     
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {                
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(
                exception.ToString());
        }

    }
}
