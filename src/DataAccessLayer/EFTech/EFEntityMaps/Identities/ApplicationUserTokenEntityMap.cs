using IdentityLayer.AspDotNetIdentity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EFTech.EFEntityMaps.Identities
{
    public class ApplicationUserTokenEntityMap :
        IEntityTypeConfiguration<ApplicationUserToken>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
        {
            builder.ToTable("ApplicationUserTokens");
        }
    }
}