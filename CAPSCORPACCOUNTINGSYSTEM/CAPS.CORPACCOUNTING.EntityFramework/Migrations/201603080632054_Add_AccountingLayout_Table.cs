namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AccountingLayout_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_AccountingLayout",
                c => new
                    {
                        AccountingLayoutId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 500),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        TypeOfLayoutId = c.Int(),
                        CopyTypeOfHeadingGroupId = c.Short(),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        EntityId = c.Int(nullable: false),
                        CopyTrxId = c.Int(),
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
                    { "DynamicFilter_AccountingLayoutUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountingLayoutUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccountingLayoutId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_AccountingLayout",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AccountingLayoutUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountingLayoutUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
