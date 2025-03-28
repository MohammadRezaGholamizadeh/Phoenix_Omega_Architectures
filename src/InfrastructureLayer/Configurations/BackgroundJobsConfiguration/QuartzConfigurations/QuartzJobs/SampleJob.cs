using Quartz;

namespace InfrastructureLayer.Configurations.BackgroundJobsConfiguration.QuartzConfigurations.QuartzJobs
{
    public class SampleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Job Status : Run Successfully !!!");
            Console.ForegroundColor = ConsoleColor.White;
            return Task.CompletedTask;
        }
    }
}
