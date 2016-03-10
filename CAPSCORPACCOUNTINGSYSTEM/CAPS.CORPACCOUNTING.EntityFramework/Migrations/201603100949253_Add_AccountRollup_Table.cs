namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AccountRollup_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_AccountRollup",
                c => new
                    {
                        AccountRollupId = c.Int(nullable: false, identity: true),
                        ChartOfAccountRollupId = c.Int(nullable: false),
                        AccountId = c.Long(nullable: false),
                        LinkAccountId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
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
                    { "DynamicFilter_AccountRollupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountRollupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccountRollupId)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId, cascadeDelete: false)
                .ForeignKey("dbo.CAPS_ChartOfAccountRollup", t => t.ChartOfAccountRollupId, cascadeDelete: true)
                .Index(t => t.ChartOfAccountRollupId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_AccountRollup", "ChartOfAccountRollupId", "dbo.CAPS_ChartOfAccountRollup");
            DropForeignKey("dbo.CAPS_AccountRollup", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_AccountRollup", new[] { "AccountId" });
            DropIndex("dbo.CAPS_AccountRollup", new[] { "ChartOfAccountRollupId" });
            DropTable("dbo.CAPS_AccountRollup",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AccountRollupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountRollupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
