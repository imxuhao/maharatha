namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BankStatementDetail_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BankStatementDetails",
                c => new
                    {
                        BankStatementDetailId = c.Int(nullable: false, identity: true),
                        BankAccountId = c.Int(nullable: false),
                        BankRecControlId = c.Int(nullable: false),
                        ClosingPeriod = c.DateTime(storeType: "smalldatetime"),
                        StartingBalance = c.Decimal(precision: 18, scale: 2),
                        Endingbalance = c.Decimal(precision: 18, scale: 2),
                        OpenBalance = c.Decimal(precision: 18, scale: 2),
                        ClearedChecks = c.Decimal(precision: 18, scale: 2),
                        ClearedDeposits = c.Decimal(precision: 18, scale: 2),
                        ClearedAdjs = c.Decimal(precision: 18, scale: 2),
                        LedgerBalance = c.Decimal(precision: 18, scale: 2),
                        GLEndBal = c.Decimal(precision: 18, scale: 2),
                        BankCharges = c.Decimal(precision: 18, scale: 2),
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
                    { "DynamicFilter_BankStatementDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankStatementDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BankStatementDetailId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_BankRecControl", t => t.BankRecControlId)
                .Index(t => t.BankAccountId)
                .Index(t => t.BankRecControlId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BankStatementDetails", "BankRecControlId", "dbo.CAPS_BankRecControl");
            DropForeignKey("dbo.CAPS_BankStatementDetails", "BankAccountId", "dbo.CAPS_BankAccount");
            DropIndex("dbo.CAPS_BankStatementDetails", new[] { "BankRecControlId" });
            DropIndex("dbo.CAPS_BankStatementDetails", new[] { "BankAccountId" });
            DropTable("dbo.CAPS_BankStatementDetails",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BankStatementDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankStatementDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
