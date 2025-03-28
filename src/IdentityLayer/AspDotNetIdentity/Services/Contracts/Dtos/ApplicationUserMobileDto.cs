using System.ComponentModel.DataAnnotations;

namespace IdentityLayer.AspDotNetIdentity.Services.Contracts.Dtos
{
    public class ApplicationUserMobileDto
    {
        [Required]
        [MaxLength(10)]
        public string MobileNumber { get; set; }
        [Required]
        [MaxLength(3)]
        public string CountryCallingCode { get; set; }
    }
}
