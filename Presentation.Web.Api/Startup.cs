using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Application.MainModule.DTO.AppSettings;
using Application.MainModule.DTO.Mappings;
using Application.MainModule.IServices;
using Application.MainModule.Services;
using AspNetCoreRateLimit;
using Domain.MainModule.IRepositories;
using FluentValidation.AspNetCore;
using Infrastructure.CrossCutting.Jwt;
using Infrastructure.CrossCutting.Logger;
using Infrastructure.Data.Core.Context;
using Infrastructure.Data.MainModule.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using Presentation.Web.Api.Filters;
using Presentation.Web.Api.Middleware;
using Presentation.Web.API.Middleware;

namespace Presentation.Web.Api
{
    public class Startup
    {
        private readonly string _allowSpecificOrigins = "_allowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/NLog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
            services.Configure<CryptoSection>(Configuration.GetSection("CryptoSection"));
            //RATE LIMIT SECTION
            services.AddMemoryCache();
            //services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitOptions>(opt =>
                {
                    opt.EnableEndpointRateLimiting = true;
                    opt.StackBlockedRequests = true;
                    opt.HttpStatusCode = (int)HttpStatusCode.TooManyRequests;
                    opt.RealIpHeader = "X-Real-IP";
                    opt.ClientIdHeader = "X-ClientId";
                    opt.QuotaExceededResponse = new QuotaExceededResponse
                    {
                        Content =
                            "{{ \"statusCode\": \"429\", \"message\": \"Whoa! Calm down, cowboy!\", \"details\": \"Quota exceeded. Maximum allowed: {0} per {1}. Please try again in {2} second(s).\" }}",
                        ContentType = "application/json",
                        StatusCode = 429
                    };
                    opt.GeneralRules = new List<RateLimitRule>
                    {
                        new()
                        {
                            Endpoint = "GET:/api/product/getAll",
                            Period = "10s",
                            Limit = 5
                        }
                    };
                }
            );
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddInMemoryRateLimiting();
            /*AUTO MAPPER SECTION*/
            services.AddAutoMapper(typeof(EntityToDtoMappingProfile), typeof(DtoToEntityMappingProfile));
            services.AddControllers();
            services.AddMvc(m => { m.Filters.Add<FluentDtoFilter>();})
                .AddFluentValidation(r => r.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            /*DATABASE SECTION*/
            services.AddDbContext<QhatuContext>(c => c.UseSqlServer(Configuration.GetConnectionString("Qhatu")));
            /*REPOSITORIES DI SECTION*/
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();

            #region UTIL DI
            services.AddTransient<IJwtManager, JwtManager>();


            #endregion

            #region SERVICES DI

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderDetailService, OrderDetailService>();


            #endregion

            #region AUTHENTICATION

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtBearer:Issuer"],
                    ValidAudience = Configuration["JwtBearer:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtBearer:SecretKey"]))
                };
            });


            #endregion

            #region SWAGGER

            services.AddSwaggerGen(doc =>
            {
                doc.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = @"Esta API utiliza autorización basada en tokens. Ingrese 'Bearer' [espacio] TOKEN"
                });
                doc.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        }, new List<string>()
                    }
                });
                doc.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Qhatu API", 
                        Version = "v1",
                        Description = "Simplify API development for users, teams, and enterprises with the Swagger open source and professional toolset. Find out how Swagger can help you design and document your APIs at scale.",
                        Contact = new OpenApiContact
                        {
                            Email = "admin@newhorizons.edu.pe",
                            Name = "Juan Pablo Perez Lopez",
                            Url = new Uri("https://swagger.io/resources/open-api/")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "License 2022",
                            Url = new Uri("https://swagger.io/resources/open-api/")
                        },
                        TermsOfService = new Uri("https://swagger.io/resources/open-api/")
                    });
            });

            #endregion

            services.AddCors(opt =>
            {
                opt.AddPolicy(_allowSpecificOrigins, builder =>
                {
                    builder.WithOrigins(Configuration.GetValue<string>("CorsSection:AllowOrigin"))
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(opt =>
                {
                    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Qhatu API");
                    opt.RoutePrefix = string.Empty;
                });
            }
            app.UseSecurityHeaders();
            app.UseCors(_allowSpecificOrigins);
            //app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseIpRateLimiting();
            app.UseMiddleware<ExceptionMiddleware>(logger);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
