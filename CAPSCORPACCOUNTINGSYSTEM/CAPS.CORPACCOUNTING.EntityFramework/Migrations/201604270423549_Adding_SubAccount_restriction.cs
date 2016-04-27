namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_SubAccount_restriction : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.CAPS_SubAccount",
            //    c => new
            //        {
            //            SubAccountId = c.Long(nullable: false, identity: true),
            //            LajitId = c.Long(),
            //            Description = c.String(nullable: false, maxLength: 100),
            //            Caption = c.String(maxLength: 20),
            //            DisplaySequence = c.Short(),
            //            SubAccountNumber = c.String(maxLength: 100),
            //            AccountingLayoutItemId = c.Int(),
            //            GroupCopyLabel = c.String(maxLength: 20),
            //            IsAccountSpecific = c.Boolean(nullable: false),
            //            IsMandatory = c.Boolean(nullable: false),
            //            IsBudgetInclusive = c.Boolean(nullable: false),
            //            IsCorporateSubAccount = c.Boolean(nullable: false),
            //            IsProjectSubAccount = c.Boolean(nullable: false),
            //            EntityId = c.Int(nullable: false),
            //            IsApproved = c.Boolean(nullable: false),
            //            IsActive = c.Boolean(nullable: false),
            //            TypeOfInactiveStatusId = c.Int(),
            //            IsEnterable = c.Boolean(),
            //            SearchOrder = c.Long(),
            //            SearchNo = c.String(maxLength: 50),
            //            OrganizationUnitId = c.Long(),
            //            TenantId = c.Int(nullable: false),
            //            TypeofSubAccountId = c.Int(nullable: false),
            //            IsDeleted = c.Boolean(nullable: false),
            //            DeleterUserId = c.Long(),
            //            DeletionTime = c.DateTime(),
            //            LastModificationTime = c.DateTime(),
            //            LastModifierUserId = c.Long(),
            //            CreationTime = c.DateTime(nullable: false),
            //            CreatorUserId = c.Long(),
            //        },
            //    annotations: new Dictionary<string, object>
            //    {
            //        { "DynamicFilter_SubAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
            //        { "DynamicFilter_SubAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
            //    })
            //    .PrimaryKey(t => t.SubAccountId);
            
            CreateTable(
                "dbo.CAPS_SubAccountAccessControl",
                c => new
                    {
                        SubAccountId = c.Long(nullable: false),
                        SecureId = c.Int(),
                        UserId = c.Long(),
                        IsAccessProvided = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SubAccountRestrictionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SubAccountRestrictionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.SubAccountId)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId)
                .ForeignKey("dbo.CAPS_SecureGroup", t => t.SecureId)
                .ForeignKey("dbo.CAPS_Users", t => t.UserId)
                .Index(t => t.SubAccountId)
                .Index(t => t.SecureId)
                .Index(t => t.UserId);
            
            //DropTable("dbo.CAPS_SubAccount",
            //    removedAnnotations: new Dictionary<string, object>
            //    {
            //        { "DynamicFilter_SubAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
            //        { "DynamicFilter_SubAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
            //    });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CAPS_SubAccount",
                c => new
                    {
                        SubAccountId = c.Long(nullable: false, identity: true),
                        LajitId = c.Long(),
                        Description = c.String(nullable: false, maxLength: 100),
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
                        TypeOfInactiveStatusId = c.Int(),
                        IsEnterable = c.Boolean(),
                        SearchOrder = c.Long(),
                        SearchNo = c.String(maxLength: 50),
                        OrganizationUnitId = c.Long(),
                        TenantId = c.Int(nullable: false),
                        TypeofSubAccountId = c.Int(nullable: false),
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
            
            DropForeignKey("dbo.CAPS_SubAccountAccessControl", "UserId", "dbo.CAPS_Users");
            DropForeignKey("dbo.CAPS_SubAccountAccessControl", "SecureId", "dbo.CAPS_SecureGroup");
            DropForeignKey("dbo.CAPS_SubAccountAccessControl", "SubAccountId", "dbo.CAPS_SubAccount");
            DropIndex("dbo.CAPS_SubAccountAccessControl", new[] { "UserId" });
            DropIndex("dbo.CAPS_SubAccountAccessControl", new[] { "SecureId" });
            DropIndex("dbo.CAPS_SubAccountAccessControl", new[] { "SubAccountId" });
            DropTable("dbo.CAPS_SubAccountAccessControl",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SubAccountRestrictionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SubAccountRestrictionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_SubAccount",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SubAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SubAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
