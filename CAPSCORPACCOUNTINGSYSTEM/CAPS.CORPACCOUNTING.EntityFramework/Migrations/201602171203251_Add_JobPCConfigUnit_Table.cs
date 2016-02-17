namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobPCConfigUnit_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobPCConfig",
                c => new
                    {
                        JobPCConfigId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        AccountId = c.Long(nullable: false),
                        VendorId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        OrganizationUnitId = c.Long(),
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
                    { "DynamicFilter_JobPCConfigUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobPCConfigUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobPCConfigId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.AccountId)
                .Index(t => t.VendorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobPCConfig", "VendorId", "dbo.CAPS_Vendors");
            DropForeignKey("dbo.CAPS_JobPCConfig", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_JobPCConfig", "AccountId", "dbo.CAPS_Accounts");
            DropIndex("dbo.CAPS_JobPCConfig", new[] { "VendorId" });
            DropIndex("dbo.CAPS_JobPCConfig", new[] { "AccountId" });
            DropIndex("dbo.CAPS_JobPCConfig", new[] { "JobId" });
            DropTable("dbo.CAPS_JobPCConfig",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobPCConfigUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobPCConfigUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
