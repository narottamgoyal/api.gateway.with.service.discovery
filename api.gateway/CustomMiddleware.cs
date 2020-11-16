using Microsoft.AspNetCore.Http;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.gateway
{
    public class CustomMiddleware : OcelotPipelineConfiguration
    {
        public CustomMiddleware()
        {
            PreAuthorisationMiddleware = async (ctx, next) =>
            {
                await ProcessRequest(ctx.HttpContext, next);
            };
        }

        public async Task ProcessRequest(HttpContext context, System.Func<Task> next)
        {
            bool dd = context.User.Identity.IsAuthenticated;
            var x = 4;


            // Get the the any service object, if required
            //var anyService = context.RequestServices.GetService(typeof(<Service class reference>));

            var user = ((DefaultHttpContext)context)?.User;
            var email = user.Claims.Where(y => y.Type.Contains("email")).FirstOrDefault()?.Value;
            
            
            if (!string.IsNullOrWhiteSpace(email) && email.Equals("BobSmith66@email.com", StringComparison.CurrentCultureIgnoreCase))
            {
                // Example 1 : adding extra claims
                EnrichClaim(user);
            }
            else if (1 == x)
            {
                // Example 2 : Return the request 
                await ReturnStatus(context, HttpStatusCode.Unauthorized, "does not have right scope");
                return;
            }
            else if (2 == x)
            {
                await ReturnStatus(context, HttpStatusCode.InternalServerError, "some error");
                return;
            }

            // Call the underline service
            await next.Invoke();
        }

        private void EnrichClaim(ClaimsPrincipal claims)
        {
            var listOfClaims = new List<Claim>
            {
                new Claim("CustomClaimName", "CustomClaimValue")
            };

            claims.AddIdentity(new ClaimsIdentity(listOfClaims));
        }

        private static async Task ReturnStatus(HttpContext context, HttpStatusCode statusCode, string msg)
        {
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(msg);
        }
    }
}