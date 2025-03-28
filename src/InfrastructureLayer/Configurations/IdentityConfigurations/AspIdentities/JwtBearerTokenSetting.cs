namespace InfrastructureLayer.Configurations.IdentityConfigurations.AspIdentities
{
    public class JwtBearerTokenSetting
    {
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpiryTimeInSeconds { get; set; }
        public int RefreshTokenExpiryTimeInSeconds { get; set; }
        public bool RequireHttpsMetadata { get; set; }
        public bool SaveToken { get; set; }
    }
}
