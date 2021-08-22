﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace src.Middleware
{
    public class CustomerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;
            var method = context.Request.Method;
            if (CheckUrl(path) && method.Equals("GET"))
            {
                var session = context.Session.GetString("customer");
                if (session is null)
                {
                    context.Request.Path = "/Auth/Login";
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
            return path.Contains("/Customer") && !path.Contains("/Delete/");
        }
    }
}
