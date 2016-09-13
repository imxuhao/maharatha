namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ChartOfAccountRollup_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ChartOfAccountRollup",
                c => new
                    {
                        ChartOfAccountRollupId = c.Int(nullable: false, identity: true),
                        ChartOfAccountId = c.Int(nullable: false),
                        LinkChartOfAccountId = c.Int(),
                        Description = c.String(maxLength: 100),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(),
                        TypeOfInActiveStatusId = c.Int(),
                        TypeOfValidationCategoryId = c.Short(),
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
                    { "DynamicFilter_ChartOfAccountRollupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ChartOfAccountRollupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ChartOfAccountRollupId)
                .ForeignKey("dbo.CAPS_ChartOfAccount", t => t.ChartOfAccountId, cascadeDelete: true)
                .Index(t => t.ChartOfAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ChartOfAccountRollup", "ChartOfAccountId", "dbo.CAPS_ChartOfAccount");
            DropIndex("dbo.CAPS_ChartOfAccountRollup", new[] { "ChartOfAccountId" });
            DropTable("dbo.CAPS_ChartOfAccountRollup",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ChartOfAccountRollupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ChartOfAccountRollupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
