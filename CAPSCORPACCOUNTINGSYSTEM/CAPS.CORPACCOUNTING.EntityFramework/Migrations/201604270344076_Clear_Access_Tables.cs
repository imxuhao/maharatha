namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Clear_Access_Tables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAPS_AccountAccessControl", "AccountId", "dbo.CAPS_Account");
            DropForeignKey("dbo.CAPS_AccountAccessControl", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_AccountAccessControl", "SecureId", "dbo.CAPS_SecureGroup");
            DropForeignKey("dbo.CAPS_AccountAccessControl", "UserId", "dbo.CAPS_Users");
            DropForeignKey("dbo.CAPS_JobAccessControl", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_JobAccessControl", "SecureId", "dbo.CAPS_SecureGroup");
            DropForeignKey("dbo.CAPS_JobAccessControl", "UserId", "dbo.CAPS_Users");
            DropForeignKey("dbo.CAPS_SecureGroupMappingUnit", "SecureGroupID", "dbo.CAPS_SecureGroup");
            DropForeignKey("dbo.CAPS_SecureGroupMappingUnit", "RoleId", "dbo.CAPS_Roles");
            DropForeignKey("dbo.CAPS_SecureGroupMappingUnit", "UserId", "dbo.CAPS_Users");
            DropIndex("dbo.CAPS_AccountAccessControl", new[] { "AccountId" });
            DropIndex("dbo.CAPS_AccountAccessControl", new[] { "JobId" });
            DropIndex("dbo.CAPS_AccountAccessControl", new[] { "SecureId" });
            DropIndex("dbo.CAPS_AccountAccessControl", new[] { "UserId" });
            DropIndex("dbo.CAPS_JobAccessControl", new[] { "JobId" });
            DropIndex("dbo.CAPS_JobAccessControl", new[] { "SecureId" });
            DropIndex("dbo.CAPS_JobAccessControl", new[] { "UserId" });
            DropIndex("dbo.CAPS_SecureGroupMappingUnit", new[] { "SecureGroupID" });
            DropIndex("dbo.CAPS_SecureGroupMappingUnit", new[] { "RoleId" });
            DropIndex("dbo.CAPS_SecureGroupMappingUnit", new[] { "UserId" });
            DropTable("dbo.CAPS_SecureGroup",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SecureGroup_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SecureGroup_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_AccountAccessControl",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AccountAccessRestrictionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountAccessRestrictionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_JobAccessControl",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobAccessRestrictionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobAccessRestrictionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_SecureGroupMappingUnit",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SecureGroupMappingUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SecureGroupMappingUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.SecureGroupID);
            
            CreateTable(
                "dbo.CAPS_JobAccessControl",
                c => new
                    {
                        JobId = c.Int(nullable: false),
                        SecureId = c.Int(),
                        UserId = c.Long(),
                        IsAccessProvided = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobAccessRestrictionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobAccessRestrictionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobId);
            
            CreateTable(
                "dbo.CAPS_AccountAccessControl",
                c => new
                    {
                        AccountId = c.Long(nullable: false),
                        JobId = c.Int(nullable: false),
                        SecureId = c.Int(),
                        UserId = c.Long(),
                        IsAccessProvided = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AccountAccessRestrictionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountAccessRestrictionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccountId);
            
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
            
            CreateIndex("dbo.CAPS_SecureGroupMappingUnit", "UserId");
            CreateIndex("dbo.CAPS_SecureGroupMappingUnit", "RoleId");
            CreateIndex("dbo.CAPS_SecureGroupMappingUnit", "SecureGroupID");
            CreateIndex("dbo.CAPS_JobAccessControl", "UserId");
            CreateIndex("dbo.CAPS_JobAccessControl", "SecureId");
            CreateIndex("dbo.CAPS_JobAccessControl", "JobId");
            CreateIndex("dbo.CAPS_AccountAccessControl", "UserId");
            CreateIndex("dbo.CAPS_AccountAccessControl", "SecureId");
            CreateIndex("dbo.CAPS_AccountAccessControl", "JobId");
            CreateIndex("dbo.CAPS_AccountAccessControl", "AccountId");
            AddForeignKey("dbo.CAPS_SecureGroupMappingUnit", "UserId", "dbo.CAPS_Users", "Id");
            AddForeignKey("dbo.CAPS_SecureGroupMappingUnit", "RoleId", "dbo.CAPS_Roles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CAPS_SecureGroupMappingUnit", "SecureGroupID", "dbo.CAPS_SecureGroup", "SecureGroupID");
            AddForeignKey("dbo.CAPS_JobAccessControl", "UserId", "dbo.CAPS_Users", "Id");
            AddForeignKey("dbo.CAPS_JobAccessControl", "SecureId", "dbo.CAPS_SecureGroup", "SecureGroupID");
            AddForeignKey("dbo.CAPS_JobAccessControl", "JobId", "dbo.CAPS_Job", "JobId");
            AddForeignKey("dbo.CAPS_AccountAccessControl", "UserId", "dbo.CAPS_Users", "Id");
            AddForeignKey("dbo.CAPS_AccountAccessControl", "SecureId", "dbo.CAPS_SecureGroup", "SecureGroupID");
            AddForeignKey("dbo.CAPS_AccountAccessControl", "JobId", "dbo.CAPS_Job", "JobId", cascadeDelete: true);
            AddForeignKey("dbo.CAPS_AccountAccessControl", "AccountId", "dbo.CAPS_Account", "AccountId");
        }
    }
}
