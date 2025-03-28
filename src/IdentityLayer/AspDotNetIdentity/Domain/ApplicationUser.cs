using Microsoft.AspNetCore.Identity;

namespace IdentityLayer.AspDotNetIdentity.Domain
{
    public class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser(Mobile mobile, DateTime creationDate)
        {
            Mobile = mobile;
            CreationDate = creationDate;
        }
        public ApplicationUser()
        {

        }
        public Mobile Mobile { get; set; }
        public DateTime CreationDate { get; set; }
        public ApplicationUserRefreshToken? RefreshToken { get; set; }
    }
}