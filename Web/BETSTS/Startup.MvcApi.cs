using BETSTS.Core.Models.Constants;
using BETSTS.Core.Validators;
using BETSTS.Filters.Exception;
using BETSTS.Filters.Validation;
using BETSTS.Models;
using Elect.Core.EnvUtils;
using Elect.Data.IO;
using Elect.Web.Middlewares.MinResponseMiddleware;
using Elect.Web.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BETSTS
{
    public static class StartupMvcApi
    {
        #region Properties

        public const string AreaFolderName = "Areas";

        public static readonly List<StaticFileModel> ListStaticFile = new List<StaticFileModel>
        {
            new StaticFileModel
            {
                AreaFolderName = string.Empty,
                AreaName = string.Empty,
                FolderRelativePath = "wwwroot",
                HttpRequestPath = string.Empty,
                MaxAgeResponseHeader = new TimeSpan(365, 0, 0)
            }
            //},

            //new StaticFileModel
            //{
            //    AreaFolderName = AreaFolderName,
            //    AreaName = "Portal",
            //    FolderRelativePath = "wwwroot",
            //    HttpRequestPath = "/portal",
            //    MaxAgeResponseHeader = new TimeSpan(365, 0, 0)
            //}
        };

        #endregion

        #region Services

        public static IServiceCollection AddMvcApi(this IServiceCollection services)
        {
            if (!EnvHelper.IsDevelopment())
            {
                services.AddResponseCaching();
                services.AddElectMinResponse();
            }

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

            // Validation Filters

            services.AddScoped<ApiValidationActionFilterAttribute>();

            // Exception Filters

            services.AddScoped<ApiExceptionFilterAttribute>();
            services.AddScoped<RootExceptionFilterAttribute>();

            // MVC

            var mvcBuilder = services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = false; // false by default
                options.Filters.Add(new ProducesAttribute(ContentType.Json));
                options.Filters.Add(new ProducesAttribute(ContentType.Xml));
            });

            // Xml Config
            mvcBuilder.AddXmlDataContractSerializerFormatters();

            // Json Config
            mvcBuilder.AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling =
                    Formatting.JsonSerializerSettings.ReferenceLoopHandling;
                options.SerializerSettings.NullValueHandling = Formatting.JsonSerializerSettings.NullValueHandling;
                options.SerializerSettings.Formatting = Formatting.JsonSerializerSettings.Formatting;
                options.SerializerSettings.ContractResolver = Formatting.JsonSerializerSettings.ContractResolver;
                options.SerializerSettings.DateFormatString = Formatting.JsonSerializerSettings.DateFormatString;
                options.SerializerSettings.Culture = Formatting.JsonSerializerSettings.Culture;
            });

            // Validation
            mvcBuilder.AddViewOptions(options => { options.HtmlHelperOptions.ClientValidationEnabled = true; });
            mvcBuilder.AddFluentValidation(fvc => { fvc.RegisterValidatorsFromAssemblyContaining<IValidator>(); });

            // AreaName Support
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/" + AreaFolderName + "/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/" + AreaFolderName + "/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

            return services;
        }

        /// <summary>
        ///     Configures the anti-forgery tokens for better security. 
        /// </summary>
        /// <param name="services"></param>
        /// <remarks> See: http://www.asp.net/mvc/overview/security/xsrfcsrf-prevention-in-aspnet-mvc-and-web-pages </remarks>
        public static IServiceCollection AddAntiforgeryToken(this IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                // Rename the Anti-Forgery cookie from "__RequestVerificationToken" to "ape".
                // This adds a little security through obscurity and also saves sending a few
                // characters over the wire.
                options.Cookie.Name = nameof(Assembly.GetExecutingAssembly);

                // Rename the form input name from "__RequestVerificationToken" to "ape" for the
                // same reason above e.g.
                // <input name="__RequestVerificationToken" type="hidden" value="..." />
                options.FormFieldName = nameof(Assembly.GetExecutingAssembly);

                // Rename the Anti-Forgery HTTP header from RequestVerificationToken to
                // X-XSRF-TOKEN. X-XSRF-TOKEN is not a standard but a common name given to this
                // HTTP header popularized by Angular.
                options.HeaderName = HeaderKey.XAntiforgeryToken;

                // If you have enabled SSL/TLS. Uncomment this line to ensure that the
                // Anti-Forgery cookie requires SSL /TLS to be sent across the wire.
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
            });

            return services;
        }

        #endregion Services

        #region Middlewares

        public static IApplicationBuilder UseMvcApi(this IApplicationBuilder app)
        {
            if (!EnvHelper.IsDevelopment())
            {
                app.UseResponseCaching();
                app.UseElectMinResponse();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            // Root Path and GZip
            foreach (var staticFileModel in ListStaticFile)
            {
                var folderPath =
                    Path.Combine(
                        staticFileModel.AreaFolderName,
                        staticFileModel.AreaName,
                        staticFileModel.FolderRelativePath);

                folderPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);

                if (!Directory.Exists(folderPath))
                {
                    folderPath = PathHelper.GetFullPath(folderPath);
                }

             
            }

            // Http Status Code Handle
            app.UseStatusCodeHandle();

            // Config Global Route
            app.UseMvc(routes =>
            {
                routes.MapRoute("area", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            return app;
        }

        public static IApplicationBuilder UseStatusCodeHandle(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(context =>
            {
                string requestPath = context.HttpContext.Request.Path.Value.ToLowerInvariant().Trim('/');

                // Handle static resource in advance
                var listResourceRelativeUrl = ListStaticFile.SelectMany(x => x.GetListRelativeUrl()).ToList();

                List<string> listStaticResourceExtension = new List<string>
                {
                    ".css",
                    ".css.map",
                    ".scss",
                    ".less",
                    ".js",
                    ".js.map",
                    ".json",
                    ".rss",
                    ".xml",
                    ".mp3",
                    ".mp4",
                    ".ogg",
                    ".ogv",
                    ".webm",
                    ".svg",
                    ".svgz",
                    ".eot",
                    ".tff",
                    ".otf",
                    ".woff",
                    ".woff2",
                    ".crx",
                    ".xpi",
                    ".safariextz",
                    ".flv",
                    ".f4v",
                    ".png",
                    ".jpeg",
                    ".jpg",
                    ".bmp",
                    "ico"
                };

                //bool isRequestExisting =
                //    context.HttpContext.Request.Method.Equals(HttpMethod.Get.Method, StringComparison.OrdinalIgnoreCase)
                //    && listResourceRelativeUrl.Any(x => context.HttpContext.Request.IsRequestFor(x));

                //bool isRequestSupportExtension =
                //    context.HttpContext.Request.Method.Equals(HttpMethod.Get.Method, StringComparison.OrdinalIgnoreCase)
                //    && listStaticResourceExtension.Any(x =>
                //        requestPath.EndsWith(x, StringComparison.OrdinalIgnoreCase));

                //string apiAreaRootPath = Areas.Api.Controllers.BaseController.AreaName.Trim('/');


                return Task.CompletedTask;
            });

            return app;
        }

        #endregion Middlewares
    }
}