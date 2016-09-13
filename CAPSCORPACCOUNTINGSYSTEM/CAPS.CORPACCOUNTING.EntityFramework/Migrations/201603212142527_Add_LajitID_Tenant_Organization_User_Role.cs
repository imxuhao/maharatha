namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_LajitID_Tenant_Organization_User_Role : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.CAPS_OrganizationUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        ParentId = c.Long(),
                        Code = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(nullable: false, maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        LajitId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_OrganizationExtended_MayHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_OrganizationExtended_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.CAPS_OrganizationUnits", "LajitId", c => c.Int());
            AddColumn("dbo.CAPS_OrganizationUnits", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.CAPS_Roles", "LajitId", c => c.Int());
            AddColumn("dbo.CAPS_Users", "LajitId", c => c.Int());
            AddColumn("dbo.CAPS_Tenant", "LajitId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_Tenant", "LajitId");
            DropColumn("dbo.CAPS_Users", "LajitId");
            DropColumn("dbo.CAPS_Roles", "LajitId");
            DropColumn("dbo.CAPS_OrganizationUnits", "Discriminator");
            DropColumn("dbo.CAPS_OrganizationUnits", "LajitId");
            AlterTableAnnotations(
                "dbo.CAPS_OrganizationUnits",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        ParentId = c.Long(),
                        Code = c.String(nullable: false, maxLength: 128),
                        DisplayName = c.String(nullable: false, maxLength: 128),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        LajitId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_OrganizationExtended_MayHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_OrganizationExtended_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
