namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modidy_Job_JobCommercial_Table : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CAPS_CustomerPaymentTerms", name: "CustomerPayTemrsId", newName: "CustomerPayTermsId");
            RenameColumn(table: "dbo.CAPS_JobCommercial", name: "JobCommertialId", newName: "JobCommercialId");
            AddColumn("dbo.CAPS_Job", "TypeofProjectId", c => c.Int());
            AddColumn("dbo.CAPS_Job", "TaxRecoveryId", c => c.Int());
            AddColumn("dbo.CAPS_JobCommercial", "IsOTon", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_JobCommercial", "AgencyEmail", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_JobCommercial", "AgencyEmail");
            DropColumn("dbo.CAPS_JobCommercial", "IsOTon");
            DropColumn("dbo.CAPS_Job", "TaxRecoveryId");
            DropColumn("dbo.CAPS_Job", "TypeofProjectId");
            RenameColumn(table: "dbo.CAPS_JobCommercial", name: "JobCommercialId", newName: "JobCommertialId");
            RenameColumn(table: "dbo.CAPS_CustomerPaymentTerms", name: "CustomerPayTermsId", newName: "CustomerPayTemrsId");
        }
    }
}
