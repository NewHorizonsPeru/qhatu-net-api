using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Infrastructure.CrossCutting.Jwt;
using Microsoft.Extensions.Configuration;

namespace Presentation.Web.Api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtManager _jwtManager;
        private readonly IConfiguration _configuration;

        public JwtMiddleware(RequestDelegate next, IJwtManager jwtManager, IConfiguration configuration)
        {
            _next = next;
            _jwtManager = jwtManager;
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
                var userId = _jwtManager.ValidateToken(token);
                httpContext.Items["currentUser"] = userId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            

        }
    }
}