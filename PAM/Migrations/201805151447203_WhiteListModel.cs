namespace PAM.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class WhiteListModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WhiteLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        msisdn = c.String(),
                        PlatformTypeId = c.Int(nullable: false),
                        created_at = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        updated_at = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PlatformTypes", t => t.PlatformTypeId, cascadeDelete: true)
                .Index(t => t.PlatformTypeId);

            CreateTable(
                "dbo.PlatformTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.WhiteLists", "PlatformTypeId", "dbo.PlatformTypes");
            DropIndex("dbo.WhiteLists", new[] { "PlatformTypeId" });
            DropTable("dbo.PlatformTypes");
            DropTable("dbo.WhiteLists");
        }
    }
}
