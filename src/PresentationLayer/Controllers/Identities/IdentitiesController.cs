using IdentityLayer.AspDotNetIdentity.Domain;
using IdentityLayer.AspDotNetIdentity.Services.Contracts;
using IdentityLayer.AspDotNetIdentity.Services.Contracts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers.Identities
{
    [Route("api/v{version:apiVersion}/Identities")]
    [ApiController]
    [ApiVersion("1.0")]
    public class IdentitiesController : ControllerBase
    {
        private readonly IdentityService _identityService;

        public IdentitiesController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IdentityResult> InitializeUser(
            AddApplicationUserDto dto)
        {
            return await _identityService.InitializeUser(dto);
        }

        [HttpGet("all")]
        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return await _identityService.GetAllUsers();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<GetLoginDto> LogIn(LoginDto dto)
        {
            return await _identityService.LoginUser(dto);
        }
    }
}
