using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using StockManagement.Repository.Entities;
using Microsoft.EntityFrameworkCore.SqlServer;
using StockManagement.Service.Application.Interface;
using StockManagement.Service.Application.Services;
using StockManagement.Repository.Repository.Interface;
using StockManagement.Repository.Repository.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StockManagement.API.Filters;
using Microsoft.OpenApi.Models;

namespace StockManagement.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = Configuration["Jwt:Issuer"],
                       ValidAudience = Configuration["Jwt:Issuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                   };
               });
            services.AddMvc().AddMvcOptions(option =>
            {
                option.EnableEndpointRouting = false;
                option.Filters.Add<LoggingAction>();
                option.Filters.Add<LoggingException>();
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stock Management Service", Version = "v1" });
            });
            services.AddEntityFrameworkSqlServer().AddDbContext<StockManagementContext>(options =>
            {
                options.UseSqlServer(Configuration.GetValue<string>("ConnectionString:Current"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
                    });
            }, ServiceLifetime.Scoped);
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IStockPurchaseService, StockPurchaseService>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockPurchaseRepository, StockPurchaseRepository>();
            services.AddSingleton<LoggingAction>();
            services.AddSingleton<LoggingException>();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock Management Service V1");
                });
            }
            app.UseMiddleware<LoggingMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseMvc();
        }
    }
}
