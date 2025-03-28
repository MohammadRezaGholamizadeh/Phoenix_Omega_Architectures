using IdentityLayer.AspDotNetIdentity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EFTech.EFEntityMaps.Identities
{
    public class ApplicationUserRefreshTokenEntityMap
        : IEntityTypeConfiguration<ApplicationUserRefreshToken>
    {
        public void Configure(
            EntityTypeBuilder<ApplicationUserRefreshToken> builder)
        {
            builder.ToTable("ApplicationUserRefreshTokens");

            builder.HasKey(_ => _.UserId);

            builder.Property(_ => _.RefreshTokenExpiryTime).IsRequired(true);
            builder.Property(_ => _.Token).IsRequired(true);
        }
    }
}