using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;

namespace InfrastructureLayer.Configurations.IdentityConfigurations.TokensManager
{
    public class UserTokenAppService : UserTokenService
    {
        private readonly IHttpContextAccessor _accessor;

        public UserTokenAppService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string? UserId => GetUserIdFromJwtToken();
        public string? UserRole => GetUserRole();

        private string GetUserIdFromJwtToken()
        {
            var userId = string.Empty;
            if (_accessor.HttpContext != null)
            {
                userId =
                    _accessor.HttpContext.User.Claims
                             .FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier)?
                             .Value;
            }

            GuardAgainstInvalidUserId(userId);
            return userId!;
        }

        public string GetUserRole()
        {
            var role = string.Empty;
            if (_accessor.HttpContext != null)
            {
                role = _accessor.HttpContext.User.Claims
                                .FirstOrDefault(_ => _.Type == ClaimTypes.Role)?
                                .Value;
            }
            return role!;
        }

        public bool IsInRole(string role)
        {
            var isInRole = false;
            if (_accessor.HttpContext != null)
            {
                _accessor.HttpContext.User.IsInRole(role);
            }
            return isInRole;
        }

        public Dictionary<string, string> GetToken()
        {
            Dictionary<string, string> token
                = new Dictionary<string, string>();
            if (_accessor.HttpContext != null)
            {
                token = _accessor.HttpContext.Request.Headers
                            .Where(_ => _.Key == nameof(Authorization))
                            .ToDictionary(_ => _.Key, _ => _.Value.ToString());
            }
            return token;
        }

        public Dictionary<string, string> GetQueryParams()
        {
            Dictionary<string, string> queryParams
                = new Dictionary<string, string>();
            if (_accessor.HttpContext != null)
            {
                queryParams = _accessor.HttpContext.Request.Query
                                 .ToDictionary(_ => _.Key, _ => _.Value.ToString());
            }
            return queryParams;
        }

        private static void GuardAgainstInvalidUserId(string? userId)
        {
            if (userId == null || userId == string.Empty)
                throw new UserIdIsNotValidException();
        }
    }
    public class UserIdIsNotValidException : Exception
    {

    }
}
