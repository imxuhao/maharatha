namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SICCode_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_SICCode",
                c => new
                    {
                        SicCodeId = c.Int(nullable: false, identity: true),
                        TypeOfSicCodeId = c.Int(),
                        VendorId = c.Int(),
                        AccountId = c.Long(),
                        JobId = c.Int(),
                        EntityId = c.Int(),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(),
                        TypeOfInActiveStatusId = c.Int(),
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
                    { "DynamicFilter_SICCodeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SICCodeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.SicCodeId)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId)
                .ForeignKey("dbo.CAPS_TypeOfSicCode", t => t.TypeOfSicCodeId)
                .ForeignKey("dbo.CAPS_Vendor", t => t.VendorId)
                .Index(t => t.TypeOfSicCodeId)
                .Index(t => t.VendorId)
                .Index(t => t.AccountId)
                .Index(t => t.JobId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_SICCode", "VendorId", "dbo.CAPS_Vendor");
            DropForeignKey("dbo.CAPS_SICCode", "TypeOfSicCodeId", "dbo.CAPS_TypeOfSicCode");
            DropForeignKey("dbo.CAPS_SICCode", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_SICCode", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_SICCode", new[] { "JobId" });
            DropIndex("dbo.CAPS_SICCode", new[] { "AccountId" });
            DropIndex("dbo.CAPS_SICCode", new[] { "VendorId" });
            DropIndex("dbo.CAPS_SICCode", new[] { "TypeOfSicCodeId" });
            DropTable("dbo.CAPS_SICCode",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SICCodeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SICCodeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
