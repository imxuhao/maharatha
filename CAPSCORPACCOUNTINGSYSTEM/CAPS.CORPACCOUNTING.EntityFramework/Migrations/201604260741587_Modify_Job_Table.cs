namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Job_Table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAPS_Job", "ChartOfAccountId", "dbo.CAPS_ChartOfAccount");
            DropForeignKey("dbo.CAPS_Job", "RollupCenterId", "dbo.CAPS_CostCenter");
            DropIndex("dbo.CAPS_Job", new[] { "RollupCenterId" });
            DropIndex("dbo.CAPS_Job", new[] { "ChartOfAccountId" });
            AlterColumn("dbo.CAPS_Job", "RollupCenterId", c => c.Int());
            AlterColumn("dbo.CAPS_Job", "ChartOfAccountId", c => c.Int());
            CreateIndex("dbo.CAPS_Job", "RollupCenterId");
            CreateIndex("dbo.CAPS_Job", "ChartOfAccountId");
            AddForeignKey("dbo.CAPS_Job", "ChartOfAccountId", "dbo.CAPS_ChartOfAccount", "ChartOfAccountId");
            AddForeignKey("dbo.CAPS_Job", "RollupCenterId", "dbo.CAPS_CostCenter", "CostCenterId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_Job", "RollupCenterId", "dbo.CAPS_CostCenter");
            DropForeignKey("dbo.CAPS_Job", "ChartOfAccountId", "dbo.CAPS_ChartOfAccount");
            DropIndex("dbo.CAPS_Job", new[] { "ChartOfAccountId" });
            DropIndex("dbo.CAPS_Job", new[] { "RollupCenterId" });
            AlterColumn("dbo.CAPS_Job", "ChartOfAccountId", c => c.Int(nullable: false));
            AlterColumn("dbo.CAPS_Job", "RollupCenterId", c => c.Int(nullable: false));
            CreateIndex("dbo.CAPS_Job", "ChartOfAccountId");
            CreateIndex("dbo.CAPS_Job", "RollupCenterId");
            AddForeignKey("dbo.CAPS_Job", "RollupCenterId", "dbo.CAPS_CostCenter", "CostCenterId", cascadeDelete: false);
            AddForeignKey("dbo.CAPS_Job", "ChartOfAccountId", "dbo.CAPS_ChartOfAccount", "ChartOfAccountId", cascadeDelete: false);
        }
    }
}
