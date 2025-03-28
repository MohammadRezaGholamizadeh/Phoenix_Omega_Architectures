using IdentityLayer.AspDotNetIdentity.Domain;
using IdentityLayer.AspDotNetIdentity.Services.Contracts;
using IdentityLayer.AspDotNetIdentity.Services.Contracts.Dtos;
using IdentityLayer.AspDotNetIdentity.Services.Exceptions;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Setups.TokenManagerInterface;

namespace IdentityLayer.AspDotNetIdentity.Services
{
    public class IdentityAppService : IdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityRepository _identityRepository;
        private readonly TokenManager _tokenManager;

        public IdentityAppService(
            UserManager<ApplicationUser> userManager,
            IdentityRepository identityRepository,
            TokenManager tokenManager)
        {
            _userManager = userManager;
            _identityRepository = identityRepository;
            _tokenManager = tokenManager;
        }

        public async Task<IdentityResult> InitializeUser(
            AddApplicationUserDto dto)
        {
            if (dto.Mobile != null)
            {
                dto.Mobile.MobileNumber =
                   dto.Mobile.MobileNumber.TrimStart('0');
            }

            var applicationUser =
                new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    Mobile = new Mobile()
                    {
                        MobileNumber = dto.Mobile.MobileNumber,
                        CountryCallingCode = dto.Mobile.CountryCallingCode
                    },
                    Email = dto.Email,
                    UserName = dto.Mobile.MobileNumber
                };

            var result = await _userManager.CreateAsync(
                applicationUser,
                dto.Mobile!.MobileNumber);

            GuardAgainstInitializeUser(result);

            return result;
        }
        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return await _identityRepository.GetAllUser();
        }

        private static void GuardAgainstInitializeUser(IdentityResult result)
        {
            if (!result.Succeeded)
                throw new InitializingUserFailedException();
        }

        public async Task<GetLoginDto> LoginUser(LoginDto dto)
        {
            var user = await _identityRepository
                             .FindUserByUserNameOrEmail(dto.Username);

            GuardAgainstUserNotFound(user);
            GuardAgainstUserIsInactive(user!.LockoutEnabled);

            var refreshToken = _tokenManager.GenerateRefreshToken();
            dto.SetRefreshToken(
                refreshToken, _tokenManager.GetRefreshTokenExpiryTime());

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);


            return new GetLoginDto()
            {
                AccessToken = _tokenManager.Generate(userClaims, userRoles, user.Id),
                RefreshToken = refreshToken
            };
        }

        private static void GuardAgainstUserIsInactive(bool isLockoutEnabled)
        {
            if (isLockoutEnabled)
            {
                throw new UserIsInactiveException();
            }
        }

        private static void GuardAgainstUserNotFound(ApplicationUser? user)
        {
            if (user == null)
            {
                throw new UserNotFoundException();
            };
        }
    }
}
