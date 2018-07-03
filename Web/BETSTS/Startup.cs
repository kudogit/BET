using System;
using System.Net;
using BETSTS.Core;
using BETSTS.Core.EmailProvider.GmailProvider;
using BETSTS.Core.Models;
using Elect.Core.ConfigUtils;
using Elect.DI;
using Elect.Job.Hangfire;
using Elect.Job.Hangfire.Models;
using Elect.Logger.Logging;
using Elect.Mapper.AutoMapper;
using Elect.Web.DataTable;
using Elect.Web.Middlewares.CorsMiddleware;
using Elect.Web.Middlewares.HttpContextMiddleware;
using Elect.Web.Middlewares.MeasureProcessingTimeMiddleware;
using Elect.Web.Middlewares.MinResponseMiddleware;
using Elect.Web.Middlewares.RequestRewindMiddleware;
using Elect.Web.Middlewares.ServerInfoMiddleware;
using Elect.Web.Swagger;
using Elect.Web.Swagger.Models;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BETSTS
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // System Setting
            services.AddSystemSetting(Configuration.GetSection<SystemSettingModel>("SystemSetting"));

            services.AddElectLog();
            
            services.AddDataProtection();

            services.AddElectCors();

            services.AddElectHttpContext();

            services.AddElectServerInfo();
            
            services.AddElectMinResponse();

            services.AddElectDataTable();

            // Email Provider
            services.AddGmailProvider(Configuration.GetSection<GmailOptions>("EmailProvider"));

            // Api Document
            services.AddElectSwagger(Configuration.GetSection<ElectSwaggerOptions>("ElectSwagger"));

            services.AddElectHangfire(Configuration.GetSection<ElectHangfireOptions>("ElectHangfire"));

            // Auth
            services.AddAuth();


            //custom role
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    policy => policy.Requirements.Add(new AdminRequirement()));
                options.AddPolicy("Abc",
                    policy => policy.Requirements.Add(new AdminRequirement()));
            });





            // MVC and API
            services.AddMvcApi();

            // Mapper
            services.AddElectAutoMapper();

            // Auto Register Dependency Injection
            services.AddElectDI();
            services.PrintServiceAddedToConsole();

            // Cache
            services.AddMemoryCache();

            // Flurl Config
            FlurlHttp.Configure(config =>
            {
                config.JsonSerializer =
                    new NewtonsoftJsonSerializer(Core.Models.Constants.Formatting.JsonSerializerSettings);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
           
            app.UseElectCors();

            app.UseElectRequestRewind();

            app.UseElectHttpContext();

            app.UseElectMeasureProcessingTime();

            app.UseElectMinResponse();

            // Api Document
            app.UseElectSwagger();

            app.UseElectHangfire();

            // System Setting
            app.UseSystemSetting();

            // MVC and API
            app.UseMvcApi();
        }
    }
}