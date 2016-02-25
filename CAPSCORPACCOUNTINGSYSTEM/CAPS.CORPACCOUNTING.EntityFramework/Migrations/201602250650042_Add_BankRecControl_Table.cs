namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BankRecControl_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BankRecControl",
                c => new
                    {
                        BankRecControlId = c.Int(nullable: false, identity: true),
                        BankAccountId = c.Int(nullable: false),
                        JobId = c.Int(),
                        AccountId = c.Long(),
                        ClosingPeriod = c.DateTime(storeType: "smalldatetime"),
                        StartingBalance = c.Decimal(precision: 18, scale: 2),
                        Endingbalance = c.Decimal(precision: 18, scale: 2),
                        DateReconciled = c.DateTime(storeType: "smalldatetime"),
                        ReconciledByUserId = c.Int(),
                        IsReconciled = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
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
                    { "DynamicFilter_BankRecControlUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankRecControlUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BankRecControlId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.AccountId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId)
                .Index(t => t.BankAccountId)
                .Index(t => t.JobId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BankRecControl", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_BankRecControl", "BankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_BankRecControl", "AccountId", "dbo.CAPS_Accounts");
            DropIndex("dbo.CAPS_BankRecControl", new[] { "AccountId" });
            DropIndex("dbo.CAPS_BankRecControl", new[] { "JobId" });
            DropIndex("dbo.CAPS_BankRecControl", new[] { "BankAccountId" });
            DropTable("dbo.CAPS_BankRecControl",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BankRecControlUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankRecControlUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
