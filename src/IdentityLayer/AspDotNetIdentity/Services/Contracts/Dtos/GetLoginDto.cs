namespace IdentityLayer.AspDotNetIdentity.Services.Contracts.Dtos
{
    public class GetLoginDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsAccessGeranted { get; set; }
    }
}
