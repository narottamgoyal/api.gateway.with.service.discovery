using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace api.gateway
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            bool dd = context.User.Identity.IsAuthenticated;
            await _next(context);
        }
    }
}