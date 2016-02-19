namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_JobandJobDetail_Table : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CAPS_JobCommercial", newName: "CAPS_JobDetail");
            DropForeignKey("dbo.CAPS_Job", "RollupCenterId", "dbo.CAPS_RollupCenter");
            DropForeignKey("dbo.CAPS_Job", "ChartOfAccountId", "dbo.CAPS_COA");
            DropIndex("dbo.CAPS_Job", new[] { "RollupCenterId" });
            DropIndex("dbo.CAPS_Job", new[] { "ChartOfAccountId" });
            RenameColumn(table: "dbo.CAPS_JobDetail", name: "JobCommercialId", newName: "JobDetailId");
            AddColumn("dbo.CAPS_RollupCenter", "RollupTypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.CAPS_Job", "RollupCenterId", c => c.Int(nullable: false));
            AlterColumn("dbo.CAPS_Job", "ChartOfAccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.CAPS_Job", "RollupCenterId");
            CreateIndex("dbo.CAPS_Job", "ChartOfAccountId");
            AddForeignKey("dbo.CAPS_Job", "RollupCenterId", "dbo.CAPS_RollupCenter", "RollupCenterId");
            AddForeignKey("dbo.CAPS_Job", "ChartOfAccountId", "dbo.CAPS_COA", "COAId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_Job", "ChartOfAccountId", "dbo.CAPS_COA");
            DropForeignKey("dbo.CAPS_Job", "RollupCenterId", "dbo.CAPS_RollupCenter");
            DropIndex("dbo.CAPS_Job", new[] { "ChartOfAccountId" });
            DropIndex("dbo.CAPS_Job", new[] { "RollupCenterId" });
            AlterColumn("dbo.CAPS_Job", "ChartOfAccountId", c => c.Int());
            AlterColumn("dbo.CAPS_Job", "RollupCenterId", c => c.Int());
            DropColumn("dbo.CAPS_RollupCenter", "RollupTypeId");
            RenameColumn(table: "dbo.CAPS_JobDetail", name: "JobDetailId", newName: "JobCommercialId");
            CreateIndex("dbo.CAPS_Job", "ChartOfAccountId");
            CreateIndex("dbo.CAPS_Job", "RollupCenterId");
            AddForeignKey("dbo.CAPS_Job", "ChartOfAccountId", "dbo.CAPS_COA", "COAId");
            AddForeignKey("dbo.CAPS_Job", "RollupCenterId", "dbo.CAPS_RollupCenter", "RollupCenterId");
            RenameTable(name: "dbo.CAPS_JobDetail", newName: "CAPS_JobCommercial");
        }
    }
}
