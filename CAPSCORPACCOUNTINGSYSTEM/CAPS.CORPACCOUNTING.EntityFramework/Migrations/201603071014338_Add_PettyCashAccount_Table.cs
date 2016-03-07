namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PettyCashAccount_Table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAPS_BankAccount", "ControllingBankAccountId", "dbo.CAPS_Accounts");
            DropForeignKey("dbo.CAPS_BankAccount", "PettyCashAccountId", "dbo.CAPS_Accounts");
            DropIndex("dbo.CAPS_BankAccount", new[] { "ControllingBankAccountId" });
            DropIndex("dbo.CAPS_BankAccount", new[] { "PettyCashAccountId" });
            CreateTable(
                "dbo.CAPS_PettyCashAccount",
                c => new
                    {
                        PettyCashAccountId = c.Long(nullable: false, identity: true),
                        VendorId = c.Int(nullable: false),
                        AccountId = c.Long(nullable: false),
                        JobId = c.Int(nullable: false),
                        PayToName = c.String(maxLength: 200),
                        FloatAmount = c.Decimal(precision: 18, scale: 2),
                        IsCustodian = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusid = c.Int(),
                        IsApproved = c.Boolean(nullable: false),
                        OrganizationUnitId = c.Long(nullable: false),
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
                    { "DynamicFilter_PettyCashAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PettyCashAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.PettyCashAccountId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId)
                .Index(t => t.AccountId)
                .Index(t => t.JobId);
            
            AlterColumn("dbo.CAPS_BankAccount", "ControllingBankAccountId", c => c.Int());
            CreateIndex("dbo.CAPS_BankAccount", "ControllingBankAccountId");
            AddForeignKey("dbo.CAPS_BankAccount", "ControllingBankAccountId", "dbo.CAPS_BankAccount", "BankAccountId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_PettyCashAccount", "VendorId", "dbo.CAPS_Vendors");
            DropForeignKey("dbo.CAPS_PettyCashAccount", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_PettyCashAccount", "AccountId", "dbo.CAPS_Accounts");
            DropForeignKey("dbo.CAPS_BankAccount", "ControllingBankAccountId", "dbo.CAPS_BankAccount");
            DropIndex("dbo.CAPS_PettyCashAccount", new[] { "JobId" });
            DropIndex("dbo.CAPS_PettyCashAccount", new[] { "AccountId" });
            DropIndex("dbo.CAPS_PettyCashAccount", new[] { "VendorId" });
            DropIndex("dbo.CAPS_BankAccount", new[] { "ControllingBankAccountId" });
            AlterColumn("dbo.CAPS_BankAccount", "ControllingBankAccountId", c => c.Long());
            DropTable("dbo.CAPS_PettyCashAccount",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PettyCashAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PettyCashAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            CreateIndex("dbo.CAPS_BankAccount", "PettyCashAccountId");
            CreateIndex("dbo.CAPS_BankAccount", "ControllingBankAccountId");
            AddForeignKey("dbo.CAPS_BankAccount", "PettyCashAccountId", "dbo.CAPS_Accounts", "AccountId");
            AddForeignKey("dbo.CAPS_BankAccount", "ControllingBankAccountId", "dbo.CAPS_Accounts", "AccountId");
        }
    }
}
