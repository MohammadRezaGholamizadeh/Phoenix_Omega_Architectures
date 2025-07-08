using Autofac.Extensions.DependencyInjection;
using Hangfire;
using InfrastructureLayer.Configurations.AutoFacConfigurations;
using InfrastructureLayer.Configurations.ConfigurationsJson;
using InfrastructureLayer.Configurations.PresentationLayerConfigurations.ExtensionConfigurations;
using ServiceLayer.Setups.BusinessExceptions;

namespace InfrastructureLayer.Configurations.PresentationLayerConfigurations
{
    public static class StartupConfiguration
    {
        public static void ConfigureServices(
            IServiceCollection services,
            IConfiguration configuration)
        {
            new ServiceCollectionConfigurationSetter(services, configuration)
                .AddAutofac()
                .AddAuthorization()
                .AddCors()
                .AddRouting()
                .AddAspIdentity()
                .AddHealthChecks()
                .AddControllers()
                .AddHttpContextAccessor()
                .AddApiVersioning()
                .AddMvcCore(typeof(StartupConfiguration).Assembly)
                .AddEndpointsApiExplorer()
                .AddQuartzBackgroundJob()
                .AddHangfireBackgroundJobConfiguration()
                .AddSwaggerGen();
        }

        public static void ConfigureApplications(
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
                .UseHangfireDashboard();
        }

        public static async Task CreateHostWithAutofacConfig(string[] args)
        {
            var hostBuilder =
                Host.CreateDefaultBuilder(args)
                    .UseSerilogWithCustomizedConfiguration();

            hostBuilder.BindConfigurationJsons();

            hostBuilder.ConfigureServices((context, services) =>
            {
                hostBuilder.UseServiceProviderFactory(
                    new AutofacServiceProviderFactory(configurationBuilder
                    => AutofacConfig.Configure(
                        configurationBuilder,
                        context.Configuration)));

                ConfigureServices(services, context.Configuration);
            });

            hostBuilder
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                     webBuilder.SetHostUrlsAndEnvironment();
                     webBuilder.SetExceptionFilterForType<BusinessException>();
                     webBuilder.UseIISIntegration();
                     webBuilder.UseKestrel();

                     webBuilder.Configure((context, app) =>
                     {
                         var environment = context.HostingEnvironment;
                         ConfigureApplications(app, environment);
                     });

                 });

            await hostBuilder.Build().RunAsync();

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
