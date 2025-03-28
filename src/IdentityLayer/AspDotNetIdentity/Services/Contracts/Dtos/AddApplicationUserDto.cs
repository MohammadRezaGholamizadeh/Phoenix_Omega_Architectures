using System.ComponentModel.DataAnnotations;

namespace IdentityLayer.AspDotNetIdentity.Services.Contracts.Dtos
{
    public class AddApplicationUserDto
    {
        [Required]
        public ApplicationUserMobileDto Mobile { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
