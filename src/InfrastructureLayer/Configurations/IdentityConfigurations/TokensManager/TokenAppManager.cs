using InfrastructureLayer.Configurations.IdentityConfigurations.AspIdentities;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Setups.TokenManagerInterface;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InfrastructureLayer.Configurations.IdentityConfigurations.TokensManager
{
    public class TokenAppManager : TokenManager
    {
        private readonly JwtBearerTokenSetting _jwtBearerTokenSetting;

        public TokenAppManager(JwtBearerTokenSetting jwtBearerTokenSetting)
        {
            _jwtBearerTokenSetting = jwtBearerTokenSetting;
        }

        public string Generate(IList<Claim> userClaims, IList<string> userRoles, string applicationUserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtBearerTokenSetting.SecretKey);
            var tokenClaims = new ClaimsIdentity();
            tokenClaims.AddClaim(new Claim(ClaimTypes.NameIdentifier, applicationUserId));

            WriteUserRolesToTokenClaims(ref tokenClaims, userRoles);
            WriteUserClaimsToTokenClaims(ref tokenClaims, userClaims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = tokenClaims,
                Expires = DateTime.UtcNow.AddSeconds(
                            _jwtBearerTokenSetting.ExpiryTimeInSeconds),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key),
                                               SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtBearerTokenSetting.Audience,
                Issuer = _jwtBearerTokenSetting.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generatedRandomNumber = RandomNumberGenerator.Create())
            {
                generatedRandomNumber.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                 Encoding.UTF8.GetBytes(_jwtBearerTokenSetting.SecretKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            GuardAgainstInvalidToken(jwtSecurityToken);

            return principal;
        }

        private static void GuardAgainstInvalidToken(JwtSecurityToken? jwtSecurityToken)
        {
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
        }

        public DateTime GetRefreshTokenExpiryTime()
        {
            return DateTime.Now.ToUniversalTime()
                .AddSeconds(_jwtBearerTokenSetting.RefreshTokenExpiryTimeInSeconds);
        }

        private void WriteUserRolesToTokenClaims(
        ref ClaimsIdentity tokenClaims,
        IList<string> userRoles)
        {
            tokenClaims.AddClaims(
                userRoles.Select(_ => new Claim(ClaimTypes.Role, _)));
        }
        private void WriteUserClaimsToTokenClaims(
            ref ClaimsIdentity tokenClaims,
            IList<Claim> userClaims)
        {
            tokenClaims.AddClaims(
                userClaims.Select(_ => new Claim(_.Type, _.Value)));
        }
    }

}

