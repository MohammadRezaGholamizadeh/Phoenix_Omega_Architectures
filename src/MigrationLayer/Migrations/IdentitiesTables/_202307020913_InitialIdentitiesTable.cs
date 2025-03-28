using FluentMigrator;

namespace MigrationLayer.Migrations.IdentitiesTables
{

    [Migration(202307020913)]
    public class _202307020913_InitialIdentitiesTable : Migration
    {
        private readonly ScriptResourceManager _scriptResourceManager;

        public _202307020913_InitialIdentitiesTable(
            ScriptResourceManager sourceManager)
        {
            _scriptResourceManager = sourceManager;
        }

        public override void Up()
        {
            var script = _scriptResourceManager.Read(
                "_202307020913_InitialIdentitiesTable.sql");

            Execute.Sql(script);
        }

        public override void Down()
        {
            Delete.Table("ApplicationUserTokens");
            Delete.Table("ApplicationUserRoles");
            Delete.Table("ApplicationUserLogins");
            Delete.Table("ApplicationUserClaims");
            Delete.Table("ApplicationRoleClaims");
            Delete.Table("ApplicationRoles");
            Delete.Table("ApplicationUsers");
            Delete.Table("VerificationCodes");
        }
    }
}