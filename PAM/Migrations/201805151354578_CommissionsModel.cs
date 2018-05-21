namespace PAM.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CommissionsModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Commissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        comm_perc_fee = c.Double(nullable: false),
                        comm_perc_amount = c.Double(nullable: false),
                        comm_status = c.Boolean(nullable: false),
                        created_at = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        updated_at = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    })
                .PrimaryKey(t => t.ID);

        }

        public override void Down()
        {
            DropTable("dbo.Commissions");
        }
    }
}
