using Hangfire;
using Hangfire.SQLite;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureLayer.Configurations.BackgroundJobsConfiguration.HangfireConfigurations
{
    public static class HangfireConfiguration
    {
        public static IServiceCollection AddHangfireConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var hangfireConfig =
                configuration.GetHangfireConfiguration();

            services.AddHangfire(configuration =>
            {
                configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                             .UseSimpleAssemblyNameTypeSerializer()
                             .UseRecommendedSerializerSettings()
                             .UseCustomStorage(hangfireConfig);
            });
            return services.CreateDatabaseSchema(hangfireConfig).AddHangfireServer();
        }

        public static IServiceCollection CreateDatabaseSchema(
            this IServiceCollection services,
            HangfireConfig? configuration)
        {
            if (configuration == null)
                throw new Exception(message: "Can Not Initialized The HangFire DataBase !!!");
            else
            {
                string masterConnectionString = ChangeDatabaseName(configuration.ConnectionString, "master");
                var commandScript =
                    $"if db_id(N'{configuration.DataBaseName}') is null create database" +
                    $" [{configuration.DataBaseName}]";

                using var connection = new SqlConnection(masterConnectionString);
                using var command = new SqlCommand(commandScript, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return services;
            }
        }
        private static string ChangeDatabaseName(
            string connectionString, string databaseName)
        {
            var csb = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = databaseName
            };
            return csb.ConnectionString;
        }
        private static IGlobalConfiguration UseCustomStorage(
            this IGlobalConfiguration config,
            HangfireConfig dataAccessConfiguration)
        {
            switch (dataAccessConfiguration.DBProvider.ToLowerInvariant())
            {
                case "sqlserver":
                    {
                        config.UseSqlServerStorage(dataAccessConfiguration.ConnectionString);
                        return config;
                    }
                case "sqllite":
                    {
                        config.UseSQLiteStorage(dataAccessConfiguration.ConnectionString);
                        return config;
                    }
                default:
                    return config;

            }
        }

        public static HangfireConfig GetHangfireConfiguration(
            this IConfiguration configuration)
        {
            var hangFireConfig = new HangfireConfig();
            configuration.Bind(nameof(HangfireConfig), hangFireConfig);
            return hangFireConfig;
        }
    }
}
