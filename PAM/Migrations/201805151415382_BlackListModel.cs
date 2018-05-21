namespace PAM.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class BlackListModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlackLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        msisdn = c.String(),
                        created_at = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        updated_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropTable("dbo.BlackLists");
        }
    }
}
