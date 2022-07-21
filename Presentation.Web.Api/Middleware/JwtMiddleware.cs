using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Presentation.Web.Api.Util;

namespace Presentation.Web.Api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtUtil _jwtUtil;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IJwtUtil jwtUtil, IConfiguration configuration)
        {
            _next = next;
            _jwtUtil = jwtUtil;
            _configuration = configuration;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader != null)
            {
                var token = authorizationHeader.Split(" ").Last();
                if(!string.IsNullOrEmpty(token)) RegisterAccount(httpContext, token);
            }
            return _next(httpContext);
        }

        private void RegisterAccount(HttpContext httpContext, string token)
        {
            try
            {
                var userId = _jwtUtil.ValidateToken(token);
                httpContext.Items["currentUser"] = userId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            

        }
    }
}