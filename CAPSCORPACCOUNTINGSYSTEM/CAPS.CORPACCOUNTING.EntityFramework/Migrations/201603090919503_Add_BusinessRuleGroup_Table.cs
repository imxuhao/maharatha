namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BusinessRuleGroup_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BusinessRuleGroup",
                c => new
                    {
                        BusinessRuleGroupId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        BusinessRuleCategoryId = c.Short(nullable: false),
                        TestedByUser = c.String(),
                        DateTested = c.DateTime(storeType: "smalldatetime"),
                        ApprovedByUser = c.String(),
                        DateApproved = c.DateTime(storeType: "smalldatetime"),
                        IsActive = c.Boolean(nullable: false),
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
                    { "DynamicFilter_BusinessRuleGroupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessRuleGroupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BusinessRuleGroupId)
                .ForeignKey("dbo.CAPS_BusinessRuleCategory", t => t.BusinessRuleCategoryId, cascadeDelete: true)
                .Index(t => t.BusinessRuleCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BusinessRuleGroup", "BusinessRuleCategoryId", "dbo.CAPS_BusinessRuleCategory");
            DropIndex("dbo.CAPS_BusinessRuleGroup", new[] { "BusinessRuleCategoryId" });
            DropTable("dbo.CAPS_BusinessRuleGroup",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BusinessRuleGroupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessRuleGroupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
