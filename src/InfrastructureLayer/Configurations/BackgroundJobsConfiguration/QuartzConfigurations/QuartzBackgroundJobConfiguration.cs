using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.AspNetCore;

namespace InfrastructureLayer.Configurations.BackgroundJobsConfiguration.QuartzConfigurations
{
    public static class QuartzBackgroundJobConfiguration
    {
        public static IServiceCollection AddQuartzBackgroundJob(
            this IServiceCollection services)
        {
            services.AddQuartz(configure =>
            {
                configure.UseMicrosoftDependencyInjectionJobFactory();
                configure.ConfigureAllQuartzBackgroundJobs();
            });

            services.AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            return services;
        }
    }
}
