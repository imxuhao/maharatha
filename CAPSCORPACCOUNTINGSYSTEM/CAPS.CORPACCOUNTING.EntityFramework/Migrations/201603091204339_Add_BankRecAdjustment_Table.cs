namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BankRecAdjustment_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BankRecAdjustment",
                c => new
                    {
                        BankRecAdjustmentId = c.Int(nullable: false, identity: true),
                        BankRecControlId = c.Int(nullable: false),
                        Description = c.String(),
                        AccountId = c.Long(),
                        JobId = c.Int(),
                        SubAccountId = c.Long(),
                        AccountingDocumentId = c.Long(),
                        AdjustmentAmount = c.Decimal(precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
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
                    { "DynamicFilter_BankRecAdjustmentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankRecAdjustmentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BankRecAdjustmentId)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId)
                .ForeignKey("dbo.CAPS_AccountingDocument", t => t.AccountingDocumentId)
                .ForeignKey("dbo.CAPS_BankRecControl", t => t.BankRecControlId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId)
                .Index(t => t.BankRecControlId)
                .Index(t => t.AccountId)
                .Index(t => t.JobId)
                .Index(t => t.SubAccountId)
                .Index(t => t.AccountingDocumentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BankRecAdjustment", "SubAccountId", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_BankRecAdjustment", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_BankRecAdjustment", "BankRecControlId", "dbo.CAPS_BankRecControl");
            DropForeignKey("dbo.CAPS_BankRecAdjustment", "AccountingDocumentId", "dbo.CAPS_AccountingDocument");
            DropForeignKey("dbo.CAPS_BankRecAdjustment", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_BankRecAdjustment", new[] { "AccountingDocumentId" });
            DropIndex("dbo.CAPS_BankRecAdjustment", new[] { "SubAccountId" });
            DropIndex("dbo.CAPS_BankRecAdjustment", new[] { "JobId" });
            DropIndex("dbo.CAPS_BankRecAdjustment", new[] { "AccountId" });
            DropIndex("dbo.CAPS_BankRecAdjustment", new[] { "BankRecControlId" });
            DropTable("dbo.CAPS_BankRecAdjustment",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BankRecAdjustmentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankRecAdjustmentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
