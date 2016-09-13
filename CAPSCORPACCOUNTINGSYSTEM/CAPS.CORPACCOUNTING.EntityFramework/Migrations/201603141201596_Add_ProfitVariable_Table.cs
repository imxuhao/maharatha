namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ProfitVariable_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ProfitVariable",
                c => new
                    {
                        ProfitVariableId = c.Int(nullable: false, identity: true),
                        ProfitId = c.Int(nullable: false),
                        VariableTypeOfAmountId = c.Short(nullable: false),
                        JobId = c.Int(),
                        AccountId = c.Long(),
                        SubAccountId = c.Long(),
                        BillingTypeId = c.Int(),
                        SourceTypeOfAmountId = c.Short(nullable: false),
                        AdjustmentTypeOfAmountId = c.Short(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeofInactiveStatusId = c.Int(),
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
                    { "DynamicFilter_ProfitVariableUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ProfitVariableUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ProfitVariableId)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId)
                .Index(t => t.JobId)
                .Index(t => t.AccountId)
                .Index(t => t.SubAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ProfitVariable", "SubAccountId", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_ProfitVariable", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_ProfitVariable", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_ProfitVariable", new[] { "SubAccountId" });
            DropIndex("dbo.CAPS_ProfitVariable", new[] { "AccountId" });
            DropIndex("dbo.CAPS_ProfitVariable", new[] { "JobId" });
            DropTable("dbo.CAPS_ProfitVariable",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ProfitVariableUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ProfitVariableUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
