using IdentityLayer.AspDotNetIdentity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EFTech.EFEntityMaps.Identities
{
    public class IdentityEmailVerificationCodeEntityMap : 
        IEntityTypeConfiguration<IdentityEmailVerificationCode>
    {
        public void Configure(
            EntityTypeBuilder<IdentityEmailVerificationCode> builder)
        {
            builder.ToTable("EmailVerificationCodes");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).IsRequired(true).ValueGeneratedOnAdd();

            builder.Property(_ => _.VerificationCode).IsRequired(true);
            builder.Property(_ => _.Email).IsRequired(true);
            builder.Property(_ => _.VerificationDate).IsRequired(true);
            builder.Property(_ => _.IsVerified).IsRequired(true);
            builder.Property(_ => _.EmailResultDesc).HasMaxLength(1000);
            builder.Property(_ => _.Usage).IsRequired(true);
        }
    }
}
