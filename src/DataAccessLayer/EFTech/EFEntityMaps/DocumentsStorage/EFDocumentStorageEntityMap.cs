using DomainLayer.DocumentsStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EFTech.EFEntityMaps.DocumentsStorage
{
    class EFDocumentStorageEntityMap : IEntityTypeConfiguration<DocumentStorage>
    {
        public void Configure(EntityTypeBuilder<DocumentStorage> _)
        {
            _.ToTable("DocumentsStorage");
            _.HasKey(_ => _.Id);

            _.Property(_ => _.Id).ValueGeneratedOnAdd();
            _.Property(_ => _.FileName).HasMaxLength(50).IsRequired();
            _.Property(_ => _.Extension).HasMaxLength(10).IsRequired();
            _.Property(_ => _.Data).IsRequired();
            _.Property(_ => _.Status).IsRequired();
            _.Property(_ => _.CreationDate).IsRequired();
        }
    }
}
