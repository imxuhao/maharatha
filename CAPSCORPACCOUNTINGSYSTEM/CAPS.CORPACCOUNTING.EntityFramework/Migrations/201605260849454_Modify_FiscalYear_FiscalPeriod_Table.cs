namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_FiscalYear_FiscalPeriod_Table : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CAPS_FiscalPeriod", "OrganizationUnitId", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_FiscalYear", "OrganizationUnitId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CAPS_FiscalYear", "OrganizationUnitId", c => c.Long());
            AlterColumn("dbo.CAPS_FiscalPeriod", "OrganizationUnitId", c => c.Long());
        }
    }
}
