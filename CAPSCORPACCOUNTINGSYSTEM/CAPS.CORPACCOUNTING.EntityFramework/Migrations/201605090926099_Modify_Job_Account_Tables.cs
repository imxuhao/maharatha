namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Job_Account_Tables : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CAPS_TaxCredit");
            AddColumn("dbo.CAPS_Job", "TaxCreditId", c => c.Int());
            AlterColumn("dbo.CAPS_Account", "OrganizationUnitId", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_Job", "OrganizationUnitId", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_TaxCredit", "TaxCreditId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CAPS_TaxCredit", "TaxCreditId");
            CreateIndex("dbo.CAPS_Job", "TaxCreditId");
            AddForeignKey("dbo.CAPS_Job", "TaxCreditId", "dbo.CAPS_TaxCredit", "TaxCreditId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_Job", "TaxCreditId", "dbo.CAPS_TaxCredit");
            DropIndex("dbo.CAPS_Job", new[] { "TaxCreditId" });
            DropPrimaryKey("dbo.CAPS_TaxCredit");
            AlterColumn("dbo.CAPS_TaxCredit", "TaxCreditId", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.CAPS_Job", "OrganizationUnitId", c => c.Long());
            AlterColumn("dbo.CAPS_Account", "OrganizationUnitId", c => c.Long());
            DropColumn("dbo.CAPS_Job", "TaxCreditId");
            AddPrimaryKey("dbo.CAPS_TaxCredit", "TaxCreditId");
        }
    }
}
