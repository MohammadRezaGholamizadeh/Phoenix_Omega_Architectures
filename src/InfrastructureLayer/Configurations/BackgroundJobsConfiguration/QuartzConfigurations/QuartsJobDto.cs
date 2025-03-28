using Quartz;

namespace InfrastructureLayer.Configurations.BackgroundJobsConfiguration.QuartzConfigurations
{
    public class QuartsJobDto
    {
        public IServiceCollectionQuartzConfigurator Configurator { get; set; }
        public string JobIdentity { get; set; }
    }
}
