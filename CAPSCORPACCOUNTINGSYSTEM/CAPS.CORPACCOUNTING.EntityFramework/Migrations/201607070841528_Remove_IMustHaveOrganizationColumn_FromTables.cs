namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_IMustHaveOrganizationColumn_FromTables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CAPS_AccountingDocument", "OrganizationUnitId", c => c.Long());
            AlterColumn("dbo.CAPS_Account", "OrganizationUnitId", c => c.Long());
            AlterColumn("dbo.CAPS_ChartOfAccount", "OrganizationUnitId", c => c.Long());
            AlterColumn("dbo.CAPS_Job", "OrganizationUnitId", c => c.Long());
            AlterColumn("dbo.CAPS_PettyCashAccount", "OrganizationUnitId", c => c.Long());
            AlterColumn("dbo.CAPS_AccountingItem", "OrganizationUnitId", c => c.Long());
            AlterColumn("dbo.CAPS_FiscalPeriod", "OrganizationUnitId", c => c.Long());
            AlterColumn("dbo.CAPS_FiscalYear", "OrganizationUnitId", c => c.Long());
            AlterColumn("dbo.CAPS_JobPORangeAllocation", "OrganizationUnitId", c => c.Long());
            AlterColumn("dbo.CAPS_SubAccountRestriction", "OrganizationUnitId", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CAPS_SubAccountRestriction", "OrganizationUnitId", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_JobPORangeAllocation", "OrganizationUnitId", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_FiscalYear", "OrganizationUnitId", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_FiscalPeriod", "OrganizationUnitId", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_AccountingItem", "OrganizationUnitId", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_PettyCashAccount", "OrganizationUnitId", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_Job", "OrganizationUnitId", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_ChartOfAccount", "OrganizationUnitId", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_Account", "OrganizationUnitId", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_AccountingDocument", "OrganizationUnitId", c => c.Long(nullable: false));
        }
    }
}
