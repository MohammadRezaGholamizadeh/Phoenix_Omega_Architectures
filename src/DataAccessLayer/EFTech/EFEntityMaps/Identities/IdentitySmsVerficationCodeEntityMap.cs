using IdentityLayer.AspDotNetIdentity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EFTech.EFEntityMaps.Identities
{
    public class IdentitySmsVerficationCodeEntityMap :
        IEntityTypeConfiguration<IdentitySmsVerificationCode>
    {
        public void Configure(
            EntityTypeBuilder<IdentitySmsVerificationCode> builder)
        {
            builder.ToTable("SmsVerificationCodes");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();

            builder.Property(_ => _.VerificationDate).IsRequired(true);
            builder.Property(_ => _.VerificationCode).IsRequired(true);
            builder.Property(_ => _.IsVerified).IsRequired(true);
            builder.Property(_ => _.SmsResultDesc).HasMaxLength(1000);

            builder.OwnsOne(_ => _.Mobile, mobile =>
            {
                mobile.Property(_ => _.CountryCallingCode);
                mobile.Property(_ => _.MobileNumber);
            });
        }
    }
}