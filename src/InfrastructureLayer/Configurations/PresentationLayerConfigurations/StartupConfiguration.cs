using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using InfrastructureLayer.Configurations.ConfigurationsJson;
using InfrastructureLayer.Configurations.PresentationLayerConfigurations.ExtensionConfigurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceLayer.Setups.BusinessExceptions;
using System.Reflection;

namespace InfrastructureLayer.Configurations.PresentationLayerConfigurations
{
    public static class StartupConfiguration
    {
        public static void ConfigureServices(
            IServiceCollection services,
            IConfiguration configuration,
            Assembly presentationAssembly)
        {
            new ServiceCollectionConfigurationSetter(
                    services,
                    configuration)
                .AddAutofac()
                .AddAuthorization()
                .AddCors()
                .AddRouting()
                .AddAspIdentity()
                .AddHealthChecks()
                .AddControllers()
                .AddHttpContextAccessor()
                .AddMvcCore(presentationAssembly)
                .AddApiVersioning()
                .AddEndpointsApiExplorer()
                .AddQuartzBackgroundJob()
                .AddHangfireBackgroundJobConfiguration()
                .AddSwaggerGen();
        }

        public static void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            new ApplicationBuilderConfigurationSetter(app)
                .UseCors()
                .UseRouting()
                .UseExceptionHandler()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpointsToConfigHealthCheck()
                .UseEndpointsToMapControllers()
                .UseHealthChecks()
                .UseHttpsRedirection()
                .UseSwaggerAndSwaggerUI()
                .InitializeDataBase()
                .UseHangfireDashboard();
        }

        public static void ConfigureContainer(ContainerBuilder builder)
        {
        }

        public static void CreateHostWithAutofacConfig(
        string[] args,
        Type startupType)
        {
            var hostBuilder =
                Host.CreateDefaultBuilder(args)
                    .UseSerilogWithCustomizedConfiguration()
                    .UseServiceProviderFactory(
                        new AutofacServiceProviderFactory());

            hostBuilder.BindConfigurationJsons();
            hostBuilder
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                     webBuilder.SetHostUrlsAndEnvironment();
                     webBuilder.SetExceptionFilterForType<BusinessException>();
                     webBuilder.UseIISIntegration();
                     webBuilder.UseKestrel();
                     webBuilder.UseStartup(startupType)
                               .UseContentRoot(Directory.GetCurrentDirectory());
                 });
            hostBuilder.Build().Run();
            var hangfireOptions =
                new BackgroundJobServerOptions
                {
                    WorkerCount =
                        Environment.ProcessorCount
                };
            using var server = new BackgroundJobServer(hangfireOptions);
        }
    }
}
