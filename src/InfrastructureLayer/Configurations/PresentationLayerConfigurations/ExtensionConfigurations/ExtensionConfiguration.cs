using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace InfrastructureLayer.Configurations.PresentationLayerConfigurations.ExtensionConfigurations
{
    public static class ExtensionConfiguration
    {
        public static IHostBuilder UseSerilogWithCustomizedConfiguration(
            this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseSerilog(
                              (_, config) =>
                              {
                                  config.AuditTo
                                        .Logger(a =>
                                         {
                                             var sqlLiteDbPath =
                                                 Directory.GetCurrentDirectory()
                                                 + @"\AuditSerilog.db";

                                             a.MinimumLevel.Information()
                                               .Enrich.FromLogContext()
                                               .WriteTo.SQLite(sqlLiteDbPath)
                                               .ReadFrom.Configuration(_.Configuration);
                                         });

                                  config.MinimumLevel.Information()
                                        .Enrich.FromLogContext()
                                        .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                                        .ReadFrom.Configuration(_.Configuration);
                              });
            return hostBuilder;
        }
    }
}
