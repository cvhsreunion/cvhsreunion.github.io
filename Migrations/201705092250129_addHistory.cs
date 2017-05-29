namespace CVHSReunion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addHistory : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Histories", "userId", "historyId");
            AddColumn("dbo.Histories", "phoneNumber", c => c.String());
            AddColumn("dbo.Histories", "helpPlanning", c => c.String());
            AddColumn("dbo.Users", "phoneNumber", c => c.String());
            AddColumn("dbo.Users", "helpPlanning", c => c.String());
        }
        
        public override void Down()
        {
            AddColumn("dbo.Histories", "userId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Histories");
            DropColumn("dbo.Users", "helpPlanning");
            DropColumn("dbo.Users", "phoneNumber");
            DropColumn("dbo.Histories", "helpPlanning");
            DropColumn("dbo.Histories", "phoneNumber");
            DropColumn("dbo.Histories", "historyId");
            AddPrimaryKey("dbo.Histories", "userId");
        }
    }
}
