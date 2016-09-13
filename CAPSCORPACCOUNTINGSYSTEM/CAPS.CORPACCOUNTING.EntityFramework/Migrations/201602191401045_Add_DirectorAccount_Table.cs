namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_DirectorAccount_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_DirectorAccount",
                c => new
                    {
                        DirectorAccountId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(),
                        DirectorId = c.Int(),
                        AccountId = c.Long(nullable: false),
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
                    { "DynamicFilter_DirectorAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DirectorAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.DirectorAccountId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Director", t => t.DirectorId)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId)
                .Index(t => t.JobId)
                .Index(t => t.DirectorId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_DirectorAccount", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_DirectorAccount", "DirectorId", "dbo.CAPS_Director");
            DropForeignKey("dbo.CAPS_DirectorAccount", "AccountId", "dbo.CAPS_Accounts");
            DropIndex("dbo.CAPS_DirectorAccount", new[] { "AccountId" });
            DropIndex("dbo.CAPS_DirectorAccount", new[] { "DirectorId" });
            DropIndex("dbo.CAPS_DirectorAccount", new[] { "JobId" });
            DropTable("dbo.CAPS_DirectorAccount",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DirectorAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DirectorAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
