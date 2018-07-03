using BETSTS.Contract.Service;
using Elect.Core.EnvUtils;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Runtime.InteropServices;

namespace BETSTS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleConfig();

            IWebHost webHost = BuildWebHost(args);

            OnAppStart(webHost);

            webHost.Run();
        }

        public static void ConsoleConfig()
        {
            string welcome =
                $@"Welcome {EnvHelper.MachineName}, {PlatformServices.Default.Application.ApplicationName} v{PlatformServices.Default.Application.ApplicationVersion} - {EnvHelper.CurrentEnvironment} | {PlatformServices.Default.Application.RuntimeFramework.FullName} | {RuntimeInformation.OSDescription}";
            
            Console.Title = welcome;

            Console.WriteLine(welcome);
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder(args);

            webHostBuilder.ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Warning));

            webHostBuilder.UseStartup<Startup>();

            if (!EnvHelper.IsDevelopment())
            {
                webHostBuilder.UseIISIntegration();
            }

            var webHost = webHostBuilder.Build();

            return webHost;
        }

        private static void OnAppStart(IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                IBootstrapperService bootstrapperService = serviceProvider.GetService<IBootstrapperService>();

                bootstrapperService.InitialAsync().Wait();
            }
        }
    }
}