using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Sentry;

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

            var environment = configuration.GetValue("Environment", defaultValue: "Development");
            if (string.IsNullOrEmpty(environment))
                throw new Exception("Environment Must Have Value !!!");

            webHostBuilder.UseEnvironment(environment);

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
