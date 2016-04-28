namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Security_Access_Restriction_Modified : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_SecureGroup",
                c => new
                    {
                        SecureGroupID = c.Int(nullable: false, identity: true),
                        SecureGroupName = c.String(),
                        Description = c.String(),
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
                    { "DynamicFilter_SecureGroup_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SecureGroup_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.SecureGroupID);
            
            CreateTable(
                "dbo.CAPS_SubEntityRestrictionUnits",
                c => new
                    {
                        AccessCOntrolID = c.Long(nullable: false, identity: true),
                        CoaId = c.Int(),
                        JobId = c.Int(),
                        AccountId = c.Long(),
                        SubAccountId = c.Long(),
                        BankAccountId = c.Long(),
                        UserId = c.Long(),
                        RoleId = c.Int(),
                        SecureId = c.Int(),
                        IsAccessProvided = c.Boolean(nullable: false),
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
                    { "DynamicFilter_SubEntityAccessRestrictionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SubEntityAccessRestrictionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccessCOntrolID)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId)
                .ForeignKey("dbo.CAPS_ChartOfAccount", t => t.CoaId)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId)
                .ForeignKey("dbo.CAPS_Roles", t => t.RoleId)
                .ForeignKey("dbo.CAPS_SecureGroup", t => t.SecureId)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId)
                .ForeignKey("dbo.CAPS_Users", t => t.UserId)
                .Index(t => t.CoaId)
                .Index(t => t.JobId)
                .Index(t => t.AccountId)
                .Index(t => t.SubAccountId)
                .Index(t => t.BankAccountId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId)
                .Index(t => t.SecureId);
            
            CreateTable(
                "dbo.CAPS_SecureGroupMappingUnit",
                c => new
                    {
                        SecureGroupID = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        UserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SecureGroupMappingUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SecureGroupMappingUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.SecureGroupID)
                .ForeignKey("dbo.CAPS_SecureGroup", t => t.SecureGroupID)
                .ForeignKey("dbo.CAPS_Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Users", t => t.UserId)
                .Index(t => t.SecureGroupID)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_SecureGroupMappingUnit", "UserId", "dbo.CAPS_Users");
            DropForeignKey("dbo.CAPS_SecureGroupMappingUnit", "RoleId", "dbo.CAPS_Roles");
            DropForeignKey("dbo.CAPS_SecureGroupMappingUnit", "SecureGroupID", "dbo.CAPS_SecureGroup");
            DropForeignKey("dbo.CAPS_SubEntityRestrictionUnits", "UserId", "dbo.CAPS_Users");
            DropForeignKey("dbo.CAPS_SubEntityRestrictionUnits", "SubAccountId", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_SubEntityRestrictionUnits", "SecureId", "dbo.CAPS_SecureGroup");
            DropForeignKey("dbo.CAPS_SubEntityRestrictionUnits", "RoleId", "dbo.CAPS_Roles");
            DropForeignKey("dbo.CAPS_SubEntityRestrictionUnits", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_SubEntityRestrictionUnits", "CoaId", "dbo.CAPS_ChartOfAccount");
            DropForeignKey("dbo.CAPS_SubEntityRestrictionUnits", "BankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_SubEntityRestrictionUnits", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_SecureGroupMappingUnit", new[] { "UserId" });
            DropIndex("dbo.CAPS_SecureGroupMappingUnit", new[] { "RoleId" });
            DropIndex("dbo.CAPS_SecureGroupMappingUnit", new[] { "SecureGroupID" });
            DropIndex("dbo.CAPS_SubEntityRestrictionUnits", new[] { "SecureId" });
            DropIndex("dbo.CAPS_SubEntityRestrictionUnits", new[] { "RoleId" });
            DropIndex("dbo.CAPS_SubEntityRestrictionUnits", new[] { "UserId" });
            DropIndex("dbo.CAPS_SubEntityRestrictionUnits", new[] { "BankAccountId" });
            DropIndex("dbo.CAPS_SubEntityRestrictionUnits", new[] { "SubAccountId" });
            DropIndex("dbo.CAPS_SubEntityRestrictionUnits", new[] { "AccountId" });
            DropIndex("dbo.CAPS_SubEntityRestrictionUnits", new[] { "JobId" });
            DropIndex("dbo.CAPS_SubEntityRestrictionUnits", new[] { "CoaId" });
            DropTable("dbo.CAPS_SecureGroupMappingUnit",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SecureGroupMappingUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SecureGroupMappingUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_SubEntityRestrictionUnits",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SubEntityAccessRestrictionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SubEntityAccessRestrictionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_SecureGroup",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SecureGroup_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SecureGroup_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
