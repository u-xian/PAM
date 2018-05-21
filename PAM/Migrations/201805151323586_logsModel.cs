namespace PAM.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class logsModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        sender = c.String(),
                        receiver = c.String(),
                        request_xml = c.String(maxLength: 4000),
                        response_xml = c.String(maxLength: 4000),
                        created_At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
