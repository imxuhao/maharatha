namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ICTRelation_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ICTRelation",
                c => new
                    {
                        ICTRelationId = c.Int(nullable: false, identity: true),
                        ICTOrganizationUnitId = c.Long(nullable: false),
                        JobId = c.Int(nullable: false),
                        ARClrAccountId = c.Long(),
                        APClrAccountId = c.Long(),
                        SubAccountId = c.Long(),
                        LocationId = c.Int(),
                        VendorId = c.Int(),
                        CustomerId = c.Int(),
                        ARBillingTypeId = c.Int(),
                        IsActive = c.Boolean(nullable: false,defaultValue:true),
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
                    { "DynamicFilter_ICTRelationUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ICTRelationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ICTRelationId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.APClrAccountId)
                .ForeignKey("dbo.CAPS_ARBillingTypes", t => t.ARBillingTypeId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.ARClrAccountId)
                .ForeignKey("dbo.CAPS_Customers", t => t.CustomerId)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_LocationSet", t => t.LocationId)
                .ForeignKey("dbo.AbpOrganizationUnits", t => t.ICTOrganizationUnitId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Accounts", t => t.SubAccountId)
                .ForeignKey("dbo.CAPS_Vendors", t => t.VendorId)
                .Index(t => t.ICTOrganizationUnitId)
                .Index(t => t.JobId)
                .Index(t => t.ARClrAccountId)
                .Index(t => t.APClrAccountId)
                .Index(t => t.SubAccountId)
                .Index(t => t.LocationId)
                .Index(t => t.VendorId)
                .Index(t => t.CustomerId)
                .Index(t => t.ARBillingTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ICTRelation", "VendorId", "dbo.CAPS_Vendors");
            DropForeignKey("dbo.CAPS_ICTRelation", "SubAccountId", "dbo.CAPS_Accounts");
            DropForeignKey("dbo.CAPS_ICTRelation", "ICTOrganizationUnitId", "dbo.AbpOrganizationUnits");
            DropForeignKey("dbo.CAPS_ICTRelation", "LocationId", "dbo.CAPS_LocationSet");
            DropForeignKey("dbo.CAPS_ICTRelation", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_ICTRelation", "CustomerId", "dbo.CAPS_Customers");
            DropForeignKey("dbo.CAPS_ICTRelation", "ARClrAccountId", "dbo.CAPS_Accounts");
            DropForeignKey("dbo.CAPS_ICTRelation", "ARBillingTypeId", "dbo.CAPS_ARBillingTypes");
            DropForeignKey("dbo.CAPS_ICTRelation", "APClrAccountId", "dbo.CAPS_Accounts");
            DropIndex("dbo.CAPS_ICTRelation", new[] { "ARBillingTypeId" });
            DropIndex("dbo.CAPS_ICTRelation", new[] { "CustomerId" });
            DropIndex("dbo.CAPS_ICTRelation", new[] { "VendorId" });
            DropIndex("dbo.CAPS_ICTRelation", new[] { "LocationId" });
            DropIndex("dbo.CAPS_ICTRelation", new[] { "SubAccountId" });
            DropIndex("dbo.CAPS_ICTRelation", new[] { "APClrAccountId" });
            DropIndex("dbo.CAPS_ICTRelation", new[] { "ARClrAccountId" });
            DropIndex("dbo.CAPS_ICTRelation", new[] { "JobId" });
            DropIndex("dbo.CAPS_ICTRelation", new[] { "ICTOrganizationUnitId" });
            DropTable("dbo.CAPS_ICTRelation",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ICTRelationUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ICTRelationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
