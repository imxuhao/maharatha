namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BusinessRule_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BusinessRule",
                c => new
                    {
                        BusinessRuleId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        BusinessRuleCategoryId = c.Short(nullable: false),
                        IsSchema = c.Boolean(nullable: false),
                        SchemaId = c.Int(),
                        IsPreference = c.Boolean(nullable: false),
                        DefaultPreferenceId = c.Int(),
                        IsPrivate = c.Boolean(nullable: false),
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
                    { "DynamicFilter_BusinessRuleUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessRuleUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BusinessRuleId)
                .ForeignKey("dbo.CAPS_BusinessRuleCategory", t => t.BusinessRuleCategoryId, cascadeDelete: true)
                .Index(t => t.BusinessRuleCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BusinessRule", "BusinessRuleCategoryId", "dbo.CAPS_BusinessRuleCategory");
            DropIndex("dbo.CAPS_BusinessRule", new[] { "BusinessRuleCategoryId" });
            DropTable("dbo.CAPS_BusinessRule",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BusinessRuleUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessRuleUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
