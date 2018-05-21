namespace PAM.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class TransactionsModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        sender = c.String(),
                        receiver = c.String(),
                        tnx_type = c.String(),
                        tnxid = c.String(),
                        external_tnx_id = c.String(),
                        initial_amount = c.Double(nullable: false),
                        comm_amount = c.Double(nullable: false),
                        total_amount = c.Double(nullable: false),
                        tnx_status = c.Boolean(nullable: false),
                        created_at = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);

        }

        public override void Down()
        {
            DropTable("dbo.UserProfile");
            DropTable("dbo.Transactions");
        }
    }
}
