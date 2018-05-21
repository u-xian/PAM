namespace PAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logsModel3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Logs", "created_at", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Logs", "created_at", c => c.DateTime(nullable: false));
        }
    }
}
