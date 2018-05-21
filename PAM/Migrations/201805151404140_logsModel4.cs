namespace PAM.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class logsModel4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logs", "created_at", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
        }

        public override void Down()
        {
            AlterColumn("dbo.Logs", "created_at", c => c.String());
        }
    }
}
