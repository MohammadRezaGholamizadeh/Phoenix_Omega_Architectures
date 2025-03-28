namespace InfrastructureLayer.Configurations.IdentityConfigurations.AspIdentities
{
    public class AspIdentityConfig
    {
        public PasswordConfig PasswordConfig { get; set; }
        public JwtBearerTokenSetting JwtBearerTokenSettings { get; set; }
        public bool LockoutAllowedForNewUsers { get; set; }

    }
}
