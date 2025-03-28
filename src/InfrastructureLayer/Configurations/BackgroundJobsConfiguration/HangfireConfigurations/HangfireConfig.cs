namespace InfrastructureLayer.Configurations.BackgroundJobsConfiguration.HangfireConfigurations
{
    public class HangfireConfig
    {
        public string DBProvider { get; set; }

        public string Server { get; set; }

        public string DataBaseName { get; set; }

        public string Trusted_Connection { get; set; }

        public string UserId { get; set; }

        public string PassWord { get; set; }

        public string ConnectionString =>
         UserId != string.Empty
         ? $"server = {Server}; database = {DataBaseName}; User Id = {UserId} ; PassWord = {PassWord};"
         : $"server = {Server}; database = {DataBaseName}; Trusted_Connection = {Trusted_Connection};";
    }
}
