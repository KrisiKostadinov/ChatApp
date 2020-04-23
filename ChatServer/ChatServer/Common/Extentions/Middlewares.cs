using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ChatServer.Common.Extentions
{
    public class Middlewares
    {
        private readonly RequestDelegate next;

        public Middlewares(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var request = httpContext.Request;

            if (request.Path.StartsWithSegments("/users/chat", StringComparison.OrdinalIgnoreCase) &&
                request.Query.TryGetValue("access_token", out var accessToken))
            {
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
            }

            await next(httpContext);
        }
    }
}
