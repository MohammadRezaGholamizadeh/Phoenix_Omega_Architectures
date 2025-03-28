using InfrastructureLayer.Configurations.BackgroundJobsConfiguration.QuartzConfigurations.QuartzJobs;
using Quartz;

namespace InfrastructureLayer.Configurations.BackgroundJobsConfiguration.QuartzConfigurations
{
    public static class QuartzJobSetup
    {
        public static IServiceCollectionQuartzConfigurator ConfigureAllQuartzBackgroundJobs(
            this IServiceCollectionQuartzConfigurator configurator)
        {
            configurator.AddJob<SampleJob>("sampleJobIdentity")
                        .AddCroneTrigger("sampleTriggerIdentity", "0/59 * * * * ?");

            return configurator;
        }
    }
}
