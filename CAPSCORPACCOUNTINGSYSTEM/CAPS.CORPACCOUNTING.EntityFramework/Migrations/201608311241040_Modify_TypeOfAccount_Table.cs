namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_TypeOfAccount_Table : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.CAPS_TypeOfAccount",
                c => new
                    {
                        TypeOfAccountId = c.Int(nullable: false, identity: true),
                        LajitId = c.Int(),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        TypeOfAccountClassificationId = c.Short(),
                        IsCurrencyCodeRequired = c.Boolean(nullable: false),
                        IsPaymentType = c.Boolean(nullable: false),
                        TenantId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TypeOfAccountUnit_MayHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.CAPS_TypeOfAccount", "TenantId", c => c.Int());
            DropColumn("dbo.CAPS_TypeOfAccount", "OrganizationUnitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_TypeOfAccount", "OrganizationUnitId", c => c.Long());
            DropColumn("dbo.CAPS_TypeOfAccount", "TenantId");
            AlterTableAnnotations(
                "dbo.CAPS_TypeOfAccount",
                c => new
                    {
                        TypeOfAccountId = c.Int(nullable: false, identity: true),
                        LajitId = c.Int(),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        TypeOfAccountClassificationId = c.Short(),
                        IsCurrencyCodeRequired = c.Boolean(nullable: false),
                        IsPaymentType = c.Boolean(nullable: false),
                        TenantId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TypeOfAccountUnit_MayHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
