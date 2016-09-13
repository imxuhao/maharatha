namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_JobLocations_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_JobLocation", "JobDetailId", c => c.Int(nullable: false));
            CreateIndex("dbo.CAPS_JobLocation", "JobDetailId");
            AddForeignKey("dbo.CAPS_JobLocation", "JobDetailId", "dbo.CAPS_JobDetail", "JobDetailId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobLocation", "JobDetailId", "dbo.CAPS_JobDetail");
            DropIndex("dbo.CAPS_JobLocation", new[] { "JobDetailId" });
            DropColumn("dbo.CAPS_JobLocation", "JobDetailId");
        }
    }
}
