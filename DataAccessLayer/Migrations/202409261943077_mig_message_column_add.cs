namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_message_column_add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "messagestatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "messageremove", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "messageremove");
            DropColumn("dbo.Messages", "messagestatus");
        }
    }
}
