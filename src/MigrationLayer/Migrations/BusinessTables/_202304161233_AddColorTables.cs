using FluentMigrator;

namespace MigrationLayer.Migrations.BusinessTables
{
    [Migration(202304161233)]
    public class _202304161233_AddColorTables : Migration
    {
        public override void Down()
        {
            Delete.Table("Color");
        }

        public override void Up()
        {
            Create.Table("Colors")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Title").AsString(50).NotNullable()
                .WithColumn("ColorHex").AsString(50).NotNullable();
        }
    }
}
