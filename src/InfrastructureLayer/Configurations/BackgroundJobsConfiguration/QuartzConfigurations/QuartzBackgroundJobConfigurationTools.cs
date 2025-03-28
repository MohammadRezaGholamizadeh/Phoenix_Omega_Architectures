using InfrastructureLayer.Configurations.BackgroundJobsConfiguration.QuartzConfigurations;
using Quartz;

namespace InfrastructureLayer.Configurations.BackgroundJobsConfiguration.QuartzConfigurations
{
    public static class QuartzBackgroundJobConfigurationTools
    {
        public static QuartsJobDto AddJob<T>(
            this IServiceCollectionQuartzConfigurator configurator,
            string jobIdentity) where T : IJob
        {
            configurator.AddJob<T>(_ => _.WithIdentity(jobIdentity).Build());
            return new QuartsJobDto()
            {
                Configurator = configurator,
                JobIdentity = jobIdentity
            };
        }

        public static IServiceCollectionQuartzConfigurator AddCroneTrigger(
           this QuartsJobDto configuration,
           string triggerIdentity,
           string cronExpression)
        {
            configuration.Configurator
                         .AddTrigger(_ =>
                              _.ForJob(configuration.JobIdentity)
                               .WithIdentity(triggerIdentity)
                               .WithCronSchedule(cronExpression));

            return configuration.Configurator;
        }

        public static IServiceCollectionQuartzConfigurator AddCalendarIntervalTrigger(
           this QuartsJobDto configuration,
           string triggerIdentity,
           int interval,
           IntervalUnit intervalUnit)
        {
            configuration.Configurator
                         .AddTrigger(_ =>
                              _.ForJob(configuration.JobIdentity)
                               .WithIdentity(triggerIdentity)
                               .WithCalendarIntervalSchedule(
                                    _ => _.WithInterval(interval, intervalUnit)));

            return configuration.Configurator;
        }

        public static IServiceCollectionQuartzConfigurator AddSimpleScheduleTrigger(
          this QuartsJobDto configuration,
          string triggerIdentity,
          TimeSpan timeSpan)
        {
            configuration.Configurator
                         .AddTrigger(_ =>
                              _.ForJob(configuration.JobIdentity)
                               .WithIdentity(triggerIdentity)
                               .WithSimpleSchedule(
                                    _ => _.WithInterval(timeSpan)));

            return configuration.Configurator;
        }

        public static IServiceCollectionQuartzConfigurator AddDailyTimeIntervalScheduleTrigger(
         this QuartsJobDto configuration,
         string triggerIdentity,
         int interval,
         IntervalUnit intervalUnit)
        {
            configuration.Configurator
                         .AddTrigger(_ =>
                              _.ForJob(configuration.JobIdentity)
                               .WithIdentity(triggerIdentity)
                               .WithDailyTimeIntervalSchedule(
                                    _ => _.WithInterval(interval, intervalUnit)));

            return configuration.Configurator;
        }
    }
}
