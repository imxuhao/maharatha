namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AccountLinks_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_AccountLinks",
                c => new
                    {
                        AccountLinkID = c.Long(nullable: false, identity: true),
                        HomeAccountId = c.Long(),
                        MapAccountId = c.Long(),
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
                    { "DynamicFilter_AccountLinks_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountLinks_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccountLinkID)
                .ForeignKey("dbo.CAPS_Account", t => t.HomeAccountId)
                .ForeignKey("dbo.CAPS_Account", t => t.MapAccountId)
                .Index(t => t.HomeAccountId)
                .Index(t => t.MapAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_AccountLinks", "MapAccountId", "dbo.CAPS_Account");
            DropForeignKey("dbo.CAPS_AccountLinks", "HomeAccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_AccountLinks", new[] { "MapAccountId" });
            DropIndex("dbo.CAPS_AccountLinks", new[] { "HomeAccountId" });
            DropTable("dbo.CAPS_AccountLinks",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AccountLinks_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountLinks_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
