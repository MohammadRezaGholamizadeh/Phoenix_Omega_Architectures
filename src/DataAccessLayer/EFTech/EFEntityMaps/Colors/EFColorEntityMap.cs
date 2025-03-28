using DomainLayer.Entities.Color;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EFTech.EFEntityMaps.Colors
{
    public class EFColorEntityMap : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> _)
        {
            _.ToTable("Colors");

            _.HasKey(_ => _.Id);

            _.Property(_ => _.Title)
                .HasMaxLength(50)
                .HasColumnName("Title")
                .IsRequired();

            _.Property(_ => _.ColorHex)
                .HasMaxLength(50)
                .HasColumnName("ColorHex")
                .IsRequired();
        }
    }
}
