namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_JobLocation_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_JobLocation", "LocationId", c => c.Int(nullable: false));
            CreateIndex("dbo.CAPS_JobLocation", "LocationId");
            AddForeignKey("dbo.CAPS_JobLocation", "LocationId", "dbo.CAPS_LocationSet", "LocationSetId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobLocation", "LocationId", "dbo.CAPS_LocationSet");
            DropIndex("dbo.CAPS_JobLocation", new[] { "LocationId" });
            DropColumn("dbo.CAPS_JobLocation", "LocationId");
        }
    }
}
