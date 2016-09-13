namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AccountingLayoutItem_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ AccountingLayoutItem",
                c => new
                    {
                        AccountingLayoutItemId = c.Int(nullable: false, identity: true),
                        AccountingLayoutId = c.Int(nullable: false),
                        TypeOfAccountingLayoutId = c.Int(nullable: false),
                        TypeOfHeadingId = c.Int(nullable: false),
                        DisplaySequence = c.Short(nullable: false),
                        IsDisplayedOnFirstPage = c.Boolean(nullable: false),
                        IsHidden = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
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
                    { "DynamicFilter_AccountingLayoutItemUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountingLayoutItemUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccountingLayoutItemId)
                .ForeignKey("dbo.CAPS_TypeOfAccountingLayout", t => t.TypeOfAccountingLayoutId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_TypeOfHeading", t => t.TypeOfHeadingId, cascadeDelete: true)
                .Index(t => t.TypeOfAccountingLayoutId)
                .Index(t => t.TypeOfHeadingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ AccountingLayoutItem", "TypeOfHeadingId", "dbo.CAPS_TypeOfHeading");
            DropForeignKey("dbo.CAPS_ AccountingLayoutItem", "TypeOfAccountingLayoutId", "dbo.CAPS_TypeOfAccountingLayout");
            DropIndex("dbo.CAPS_ AccountingLayoutItem", new[] { "TypeOfHeadingId" });
            DropIndex("dbo.CAPS_ AccountingLayoutItem", new[] { "TypeOfAccountingLayoutId" });
            DropTable("dbo.CAPS_ AccountingLayoutItem",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AccountingLayoutItemUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountingLayoutItemUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
