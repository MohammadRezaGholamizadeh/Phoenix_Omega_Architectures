using Autofac;
using DataAccessLayer.EFTech.EFDataContexts;
using DataAccessLayer.EFTech.EFRepositories.Colors;
using DataAccessLayer.EFTech.UnitOfWorks;
using IdentityLayer.AspDotNetIdentity.Services;
using InfrastructureLayer.Configurations.ConfigurationsJson;
using InfrastructureLayer.Configurations.IdentityConfigurations.AspIdentities;
using InfrastructureLayer.Configurations.IdentityConfigurations.TokensManager;
using InfrastructureLayer.Configurations.MigrationLayerConfigurations.Contracts;
using MigrationLayer;
using ServiceLayer.Services.ColorService;
using ServiceLayer.Services.ColorService.Contracts;
using ServiceLayer.Setups.RepositoryInterfaces;
using ServiceLayer.Setups.ServicecInterfaces;

namespace ConfigurationLayer.InfrastructureLayerConfiguration.AutoFacConfigurations
{
    public static class AutofacConfig
    {
        public static ContainerBuilder Configure(
            ContainerBuilder builder,
            IConfiguration configuration)
        {
            var jwtBearerTokenSetting =
                configuration.GetAspIdentityConfig()
                             .JwtBearerTokenSettings;
            SystemRequirementService(builder, configuration);
            var dataAccessConfig = configuration.GetDataAccessConfig();

            builder.RegisterAssemblyTypes(typeof(ColorAppService).Assembly)
                   .AssignableTo<IService>()
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(IColorService).Assembly,
                                          typeof(IdentityAppService).Assembly,
                                          typeof(TokenAppManager).Assembly)
                   .AssignableTo<IService>()
                   .WithParameter(new TypedParameter(typeof(JwtBearerTokenSetting), jwtBearerTokenSetting))
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(EFColorRepository).Assembly)
                      .AssignableTo<IRepository>()
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.RegisterType<EFDataContext>()
                   .AsSelf()
                   .WithParameter("connectionString",
                                  dataAccessConfig.ConnectionString)
                   .InstancePerLifetimeScope();

            builder.RegisterType<EFUnitOfWork>()
                   .As<UnitOfWork>()
                   .InstancePerLifetimeScope();

            return builder;
        }

        private static void SystemRequirementService(
            ContainerBuilder builder,
            IConfiguration configuration)
        {

            builder.RegisterType<MigrationRunner>()
                .As<IMigrationRunner>()
                .SingleInstance();
        }
    }
}
