namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ControlAccount_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ControlAccount",
                c => new
                    {
                        ControlAccountId = c.Int(nullable: false, identity: true),
                        TypeOfControlAccountId = c.Int(nullable: false),
                        AccountId = c.Long(),
                        JobId = c.Int(),
                        EntityId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
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
                    { "DynamicFilter_ControlAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ControlAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ControlAccountId)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId)
                .ForeignKey("dbo.CAPS_Entity", t => t.EntityId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId)
                .Index(t => t.AccountId)
                .Index(t => t.JobId)
                .Index(t => t.EntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ControlAccount", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_ControlAccount", "EntityId", "dbo.CAPS_Entity");
            DropForeignKey("dbo.CAPS_ControlAccount", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_ControlAccount", new[] { "EntityId" });
            DropIndex("dbo.CAPS_ControlAccount", new[] { "JobId" });
            DropIndex("dbo.CAPS_ControlAccount", new[] { "AccountId" });
            DropTable("dbo.CAPS_ControlAccount",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ControlAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ControlAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
