namespace Forums.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UDbTables", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UDbTables", "Photo", c => c.Binary());
        }
    }
}
