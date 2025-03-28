using System.ComponentModel.DataAnnotations;

namespace IdentityLayer.AspDotNetIdentity.Services.Contracts.Dtos
{
    public class LoginDto
    {
        [Required]
        [MaxLength(10)]
        public string Username { get; set; }
        [Required]
        [MaxLength(10)]
        public string Password { get; set; }

        private string _refreshToken;
        private DateTime _refreshTokenExpiryTime;

        public void SetRefreshToken(
            string refreshToken,
            DateTime refreshTokenExpiryTime)
        {
            _refreshToken = refreshToken;
            _refreshTokenExpiryTime = refreshTokenExpiryTime;
        }

        public string GetRefreshToken()
        {
            return _refreshToken;
        }

        public DateTime GetRefreshTokenExpiryTime()
        {
            return _refreshTokenExpiryTime;
        }
    }
}
