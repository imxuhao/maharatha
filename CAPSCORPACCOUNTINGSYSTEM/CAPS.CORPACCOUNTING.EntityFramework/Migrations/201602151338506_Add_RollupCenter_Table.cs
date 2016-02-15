namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_RollupCenter_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_RollupCenter",
                c => new
                    {
                        RollupCenterId = c.Int(nullable: false, identity: true),
                        Caption = c.String(nullable: false, maxLength: 500),
                        AccountId = c.Long(),
                        JobId = c.Int(),
                        IsActive = c.Boolean(nullable: false,defaultValue:true),
                        IsApproved = c.Boolean(nullable: false,defaultValue:false),
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
                    { "DynamicFilter_RollupCenterUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RollupCenterUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.RollupCenterId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.AccountId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_RollupCenter", "AccountId", "dbo.CAPS_Accounts");
            DropIndex("dbo.CAPS_RollupCenter", new[] { "AccountId" });
            DropTable("dbo.CAPS_RollupCenter",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RollupCenterUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RollupCenterUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
