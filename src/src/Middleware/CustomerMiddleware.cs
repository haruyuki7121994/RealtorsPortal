using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace src.Middleware
{
    public class CustomerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly string[] listUrl = {
            "/Customer",
            "/Customer/Index",
            "/Customer/Profile",
            "/Customer/Package",
            "/Customer/Create"
        };

        public CustomerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            if (CheckUrl(path))
            {
                var session = context.Session.GetString("customer");
                if (session is null)
                {
                    await Task.Run(
                      async () => {
                          string html = "<h1>Forbidden</h1>";
                          context.Response.StatusCode = StatusCodes.Status403Forbidden;
                          await context.Response.WriteAsync(html);
                      }
                    );
                }
                else
                {
                    await _next(context);
                }
            }
            await _next(context);
        }

        private bool CheckUrl(string path)
        {
            foreach (var item in listUrl)
            {
                if (path == item)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
