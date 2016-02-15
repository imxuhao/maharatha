namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Job_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Job",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        JobNumber = c.String(nullable: false, maxLength: 50),
                        Caption = c.String(nullable: false, maxLength: 200),
                        RollupCenterId = c.Int(),
                        IsCorporateDefault = c.Boolean(nullable: false, defaultValue: false),
                        ChartOfAccountId = c.Int(),
                        RollupAccountId = c.Long(),
                        TypeOfCurrencyId = c.Int(),
                        RollupJobId = c.Int(),
                        TypeOfJobStatusId = c.Int(),
                        TypeOfBidSoftwareId = c.Int(),
                        IsApproved = c.Boolean(nullable: false,defaultValue:false),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                        IsICTDivision = c.Boolean(nullable: false, defaultValue: false),
                        OrganizationUnitId = c.Long(),
                        TenantId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.CAPS_COA", t => t.ChartOfAccountId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.RollupAccountId)
                .ForeignKey("dbo.CAPS_RollupCenter", t => t.RollupCenterId)
                .ForeignKey("dbo.CAPS_Job", t => t.RollupJobId)
                .Index(t => t.RollupCenterId)
                .Index(t => t.ChartOfAccountId)
                .Index(t => t.RollupAccountId)
                .Index(t => t.RollupJobId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_Job", "RollupJobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_Job", "RollupCenterId", "dbo.CAPS_RollupCenter");
            DropForeignKey("dbo.CAPS_Job", "RollupAccountId", "dbo.CAPS_Accounts");
            DropForeignKey("dbo.CAPS_Job", "ChartOfAccountId", "dbo.CAPS_COA");
            DropIndex("dbo.CAPS_Job", new[] { "RollupJobId" });
            DropIndex("dbo.CAPS_Job", new[] { "RollupAccountId" });
            DropIndex("dbo.CAPS_Job", new[] { "ChartOfAccountId" });
            DropIndex("dbo.CAPS_Job", new[] { "RollupCenterId" });
            DropTable("dbo.CAPS_Job",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
