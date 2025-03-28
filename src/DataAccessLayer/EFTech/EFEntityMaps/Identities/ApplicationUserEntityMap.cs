using IdentityLayer.AspDotNetIdentity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EFTech.EFEntityMaps.Identities
{
    public class ApplicationUserEntityMap :
        IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("ApplicationUsers");

            builder.Property(_ => _.Email).IsRequired(false);

            builder.Property(_ => _.UserName)
                   .HasMaxLength(50);

            builder.Property(_ => _.NormalizedUserName)
                   .HasMaxLength(50);

            builder.OwnsOne(_ => _.Mobile, _ =>
            {
                _.Property(_ => _.CountryCallingCode).IsRequired(false);
                _.Property(_ => _.MobileNumber).IsRequired(false);
            });

            builder.HasOne(_ => _.RefreshToken)
                   .WithOne(_ => _.User);

            builder.Ignore(e => e.EmailConfirmed)
                   .Ignore(e => e.NormalizedEmail)
                   .Ignore(e => e.PhoneNumber)
                   .Ignore(e => e.PhoneNumberConfirmed);
        }
    }
}