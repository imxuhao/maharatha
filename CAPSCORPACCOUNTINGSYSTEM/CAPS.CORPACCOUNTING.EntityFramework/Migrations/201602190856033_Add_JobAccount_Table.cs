namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobAccount_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobAccount",
                c => new
                    {
                        JobAccountId = c.Long(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        AccountId = c.Long(nullable: false),
                        Description = c.String(nullable: false, maxLength: 200),
                        RollupJobId = c.Int(),
                        RollupAccountId = c.Long(),
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
                    { "DynamicFilter_JobAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobAccountId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Accounts", t => t.RollupAccountId)
                .ForeignKey("dbo.CAPS_Job", t => t.RollupJobId)
                .Index(t => t.JobId)
                .Index(t => t.AccountId)
                .Index(t => t.RollupJobId)
                .Index(t => t.RollupAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobAccount", "RollupJobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_JobAccount", "RollupAccountId", "dbo.CAPS_Accounts");
            DropForeignKey("dbo.CAPS_JobAccount", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_JobAccount", "AccountId", "dbo.CAPS_Accounts");
            DropIndex("dbo.CAPS_JobAccount", new[] { "RollupAccountId" });
            DropIndex("dbo.CAPS_JobAccount", new[] { "RollupJobId" });
            DropIndex("dbo.CAPS_JobAccount", new[] { "AccountId" });
            DropIndex("dbo.CAPS_JobAccount", new[] { "JobId" });
            DropTable("dbo.CAPS_JobAccount",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
