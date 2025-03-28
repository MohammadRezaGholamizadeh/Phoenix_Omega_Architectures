using FluentMigrator;

namespace MigrationLayer.Migrations.DocumentsStorage
{
    [Migration(202601241214)]
    public class _202601241214_AddDocumentTable : Migration
    {
        public override void Up()
        {
            Create.Table("DocumentsStorage")
                 .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
                 .WithColumn("FileName").AsString(50).Nullable()
                 .WithColumn("Extension").AsString(10).Nullable()
                 .WithColumn("Data").AsBinary(int.MaxValue)
                 .WithColumn("Status").AsByte().NotNullable()
                 .WithColumn("CreationDate").AsDateTime().NotNullable();
        }
        public override void Down()
        {
            Delete.Table("DocumentsStorage");
        }


    }
}
