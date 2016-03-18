namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_BpgId_ModifytypeofFinReport_Table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAPS_TypeOfFinReport", "TypeOfCategoryId_Id", "dbo.CAPS_TypeOfCategory");
            DropIndex("dbo.CAPS_TypeOfFinReport", new[] { "TypeOfCategoryId_Id" });
            RenameColumn(table: "dbo.CAPS_TypeOfPayroll", name: "TypeOfPayroll", newName: "TypeOfPayrollId");
            RenameColumn(table: "dbo.CAPS_TypeOfDocumentConsolidation", name: "TypeOfDocumentConsolidation", newName: "TypeOfDocumentConsolidationId");
            RenameColumn(table: "dbo.CAPS_TypeOfJob", name: "TypeOfPayroll", newName: "TypeOfJobId");
            RenameColumn(table: "dbo.CAPS_TypeOfFinReport", name: "TypeOfCategoryId_Id", newName: "TypeOfCategoryId");
            AlterColumn("dbo.CAPS_TypeOfFinReport", "TypeOfCategoryId", c => c.Short(nullable: false));
            CreateIndex("dbo.CAPS_TypeOfFinReport", "TypeOfCategoryId");
            AddForeignKey("dbo.CAPS_TypeOfFinReport", "TypeOfCategoryId", "dbo.CAPS_TypeOfCategory", "TypeOfCategoryId", cascadeDelete: true);
            DropColumn("dbo.CAPS_BatchReportItem", "BusinessProcessGroupId");
            DropColumn("dbo.CAPS_GroupItem", "MaintenanceBpgid");
            DropColumn("dbo.CAPS_TypeOfFinReport", "ReportBpgid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_TypeOfFinReport", "ReportBpgid", c => c.Int());
            AddColumn("dbo.CAPS_GroupItem", "MaintenanceBpgid", c => c.Int());
            AddColumn("dbo.CAPS_BatchReportItem", "BusinessProcessGroupId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CAPS_TypeOfFinReport", "TypeOfCategoryId", "dbo.CAPS_TypeOfCategory");
            DropIndex("dbo.CAPS_TypeOfFinReport", new[] { "TypeOfCategoryId" });
            AlterColumn("dbo.CAPS_TypeOfFinReport", "TypeOfCategoryId", c => c.Short());
            RenameColumn(table: "dbo.CAPS_TypeOfFinReport", name: "TypeOfCategoryId", newName: "TypeOfCategoryId_Id");
            RenameColumn(table: "dbo.CAPS_TypeOfJob", name: "TypeOfJobId", newName: "TypeOfPayroll");
            RenameColumn(table: "dbo.CAPS_TypeOfDocumentConsolidation", name: "TypeOfDocumentConsolidationId", newName: "TypeOfDocumentConsolidation");
            RenameColumn(table: "dbo.CAPS_TypeOfPayroll", name: "TypeOfPayrollId", newName: "TypeOfPayroll");
            CreateIndex("dbo.CAPS_TypeOfFinReport", "TypeOfCategoryId_Id");
            AddForeignKey("dbo.CAPS_TypeOfFinReport", "TypeOfCategoryId_Id", "dbo.CAPS_TypeOfCategory", "TypeOfCategoryId");
        }
    }
}
