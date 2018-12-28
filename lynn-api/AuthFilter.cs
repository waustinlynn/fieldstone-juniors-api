using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lynn_api
{
    public class AuthFilter
    {
        private readonly AppSettings _settings;
        private readonly RequestDelegate _next;
        public AuthFilter(RequestDelegate next, AppSettings settings)
        {
            _next = next;
            _settings = settings;
        }
        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"];
            //horrible hack for allowing file uploads from basic primeng control until I can implement custom
            var pathValue = context.Request.Path.Value;
            if (pathValue.Contains("fileupload") || pathValue.Contains("bracket"))
            {
                await _next.Invoke(context);
                return;
            }
            if(authHeader.Count == 0)
            {
                context.Response.StatusCode = 401;
                return;
            }
            if (authHeader[0] != null && authHeader[0].StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader[0].Substring("Basic ".Length).Trim();
                if(token != _settings.BasicKey)
                {
                    context.Response.StatusCode = 401;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
            }
            await _next.Invoke(context);
        }
    }
}
