namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SubAccountRestriction_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_SubAccountRestriction",
                c => new
                    {
                        SubAccountRestrictionId = c.Long(nullable: false, identity: true),
                        AccountId = c.Long(nullable: false),
                        SubAccountId = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        OrganizationUnitId = c.Long(nullable: false),
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
                    { "DynamicFilter_SubAccountRestrictionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SubAccountRestrictionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.SubAccountRestrictionId)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.SubAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_SubAccountRestriction", "SubAccountId", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_SubAccountRestriction", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_SubAccountRestriction", new[] { "SubAccountId" });
            DropIndex("dbo.CAPS_SubAccountRestriction", new[] { "AccountId" });
            DropTable("dbo.CAPS_SubAccountRestriction",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SubAccountRestrictionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SubAccountRestrictionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
