namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ARBillingType_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ARBillingTypes",
                c => new
                    {
                        ARBillingTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 200),
                        JobId = c.Int(nullable: false),
                        AccountId = c.Long(nullable: false),
                        IsIctBillingType = c.Boolean(nullable: false, defaultValue: false),
                        IsProjectBilling = c.Boolean(nullable: false,defaultValue:false),
                        TypeofBillingId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        OrganizationUnitId = c.Long(),
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
                    { "DynamicFilter_ARBillingTypeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ARBillingTypeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ARBillingTypeId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ARBillingTypes", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_ARBillingTypes", "AccountId", "dbo.CAPS_Accounts");
            DropIndex("dbo.CAPS_ARBillingTypes", new[] { "AccountId" });
            DropIndex("dbo.CAPS_ARBillingTypes", new[] { "JobId" });
            DropTable("dbo.CAPS_ARBillingTypes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ARBillingTypeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ARBillingTypeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
