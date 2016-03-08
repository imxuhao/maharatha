namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SubAccount_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_SubAccount",
                c => new
                    {
                        SubAccountId = c.Long(nullable: false, identity: true),
                        Description = c.String(maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        SubAccountNumber = c.String(maxLength: 100),
                        AccountingLayoutItemId = c.Int(),
                        GroupCopyLabel = c.String(maxLength: 20),
                        IsAccountSpecific = c.Boolean(nullable: false),
                        IsMandatory = c.Boolean(nullable: false),
                        IsBudgetInclusive = c.Boolean(nullable: false),
                        IsCorporateSubAccount = c.Boolean(nullable: false),
                        IsProjectSubAccount = c.Boolean(nullable: false),
                        EntityId = c.Int(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Short(),
                        IsEnterable = c.Boolean(),
                        SearchOrder = c.Long(),
                        SearchNo = c.String(maxLength: 50),
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
                    { "DynamicFilter_SubAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SubAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.SubAccountId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_SubAccount",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SubAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SubAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
