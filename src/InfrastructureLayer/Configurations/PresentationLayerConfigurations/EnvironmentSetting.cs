using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sentry;
using ServiceLayer.Setups.BusinessExceptions;

namespace InfrastructureLayer.Configurations.PresentationLayerConfigurations
{
    public static class EnvironmentSetting
    {
        public static void SetHostUrlsAndEnvironment(this IWebHostBuilder webHostBuilder)
        {
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory() + "/ConfigurationsJson")
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();

            webHostBuilder.UseEnvironment(configuration.GetValue("Environment", defaultValue: "Development"));
            webHostBuilder.UseUrls(configuration.GetValue<string>("HostUrls"));
        }

        public static void SetExceptionFilterForType<T>(this IWebHostBuilder webHostBuilder) where T : Exception
        {
            webHostBuilder.UseSentry(_ =>
            {
                _.AddExceptionFilterForType<T>();
            });
        }
    }
}
