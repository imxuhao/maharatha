namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccountClassification : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.CAPS_TypeOfAccountClassification",
                c => new
                    {
                        TypeOfAccountClassificationId = c.Short(nullable: false, identity: true),
                        LajitId = c.Short(),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        IsAccountSignPositive = c.Boolean(nullable: false),
                        IsBalanceSheetAccount = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TypeOfAccountClassificationUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_TypeOfAccountClassificationUnit_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            DropColumn("dbo.CAPS_TypeOfAccountClassification", "DisplaySequence");
            DropColumn("dbo.CAPS_TypeOfAccountClassification", "TenantId");
            DropColumn("dbo.CAPS_TypeOfAccountClassification", "OrganizationUnitId");
            DropColumn("dbo.CAPS_TypeOfAccountClassification", "IsDeleted");
            DropColumn("dbo.CAPS_TypeOfAccountClassification", "DeleterUserId");
            DropColumn("dbo.CAPS_TypeOfAccountClassification", "DeletionTime");
            DropColumn("dbo.CAPS_TypeOfAccountClassification", "LastModificationTime");
            DropColumn("dbo.CAPS_TypeOfAccountClassification", "LastModifierUserId");
            DropColumn("dbo.CAPS_TypeOfAccountClassification", "CreationTime");
            DropColumn("dbo.CAPS_TypeOfAccountClassification", "CreatorUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_TypeOfAccountClassification", "CreatorUserId", c => c.Long());
            AddColumn("dbo.CAPS_TypeOfAccountClassification", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.CAPS_TypeOfAccountClassification", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.CAPS_TypeOfAccountClassification", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.CAPS_TypeOfAccountClassification", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.CAPS_TypeOfAccountClassification", "DeleterUserId", c => c.Long());
            AddColumn("dbo.CAPS_TypeOfAccountClassification", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_TypeOfAccountClassification", "OrganizationUnitId", c => c.Long());
            AddColumn("dbo.CAPS_TypeOfAccountClassification", "TenantId", c => c.Int(nullable: false));
            AddColumn("dbo.CAPS_TypeOfAccountClassification", "DisplaySequence", c => c.Short());
            AlterTableAnnotations(
                "dbo.CAPS_TypeOfAccountClassification",
                c => new
                    {
                        TypeOfAccountClassificationId = c.Short(nullable: false, identity: true),
                        LajitId = c.Short(),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        IsAccountSignPositive = c.Boolean(nullable: false),
                        IsBalanceSheetAccount = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_TypeOfAccountClassificationUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_TypeOfAccountClassificationUnit_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
    }
}
