namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Accounts_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Long(nullable: false, identity: true),
                        AccountNumber = c.String(nullable: false, maxLength: 10),
                        Caption = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 400),
                        ChartOfAccountId = c.Int(nullable: false),
                        TypeOfAccountId = c.Int(),
                        ParentId = c.Long(),
                        Code = c.String(nullable: false, maxLength: 128),
                        DisplaySequence = c.Int(),
                        IsApproved = c.Boolean(nullable: false),
                        IsDescriptionLocked = c.Boolean(nullable: false),
                        IsElimination = c.Boolean(nullable: false),
                        IsEnterable = c.Boolean(nullable: false),
                        IsRollupAccount = c.Boolean(nullable: false),
                        IsRollupOverridable = c.Boolean(nullable: false),
                        LinkAccountId = c.Int(),
                        LinkJobId = c.Int(),
                        RollupAccountId = c.Int(),
                        RollupJobId = c.Int(),
                        IsDocControlled = c.Boolean(nullable: false),
                        IsSummaryAccount = c.Boolean(nullable: false),
                        IsBalanceSheet = c.Boolean(nullable: false),
                        BalanceSheetName = c.String(maxLength: 128),
                        IsUs1120BalanceSheet = c.Boolean(nullable: false),
                        Us1120BalanceSheetName = c.String(maxLength: 128),
                        IsProfitLoss = c.Boolean(nullable: false),
                        ProfitLossName = c.String(maxLength: 128),
                        IsUs1120IncomeStmt = c.Boolean(nullable: false),
                        Us1120IncomeStmtName = c.String(maxLength: 128),
                        IsCashFlow = c.Boolean(nullable: false),
                        CashFlowName = c.String(maxLength: 128),
                        TenantId = c.Int(nullable: false),
                        OrganizationUnitId = c.Long(),
                        IsActive = c.Boolean(nullable: false),
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
                    { "DynamicFilter_AccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.ChartOfAccounts", t => t.ChartOfAccountId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.ParentId)
                .Index(t => t.ChartOfAccountId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "ParentId", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "ChartOfAccountId", "dbo.ChartOfAccounts");
            DropIndex("dbo.Accounts", new[] { "ParentId" });
            DropIndex("dbo.Accounts", new[] { "ChartOfAccountId" });
            DropTable("dbo.Accounts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
