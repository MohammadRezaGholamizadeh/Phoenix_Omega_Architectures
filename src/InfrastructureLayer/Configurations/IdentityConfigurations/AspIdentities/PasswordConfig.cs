namespace InfrastructureLayer.Configurations.IdentityConfigurations.AspIdentities
{
    public class PasswordConfig
    {
        public bool RequireNonAlphanumeric { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public int RequiredLength { get; set; }
        public bool RequireDigit { get; set; }
    }
}
