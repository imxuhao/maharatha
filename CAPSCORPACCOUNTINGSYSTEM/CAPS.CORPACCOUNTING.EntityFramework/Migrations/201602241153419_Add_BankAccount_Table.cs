namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BankAccount_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BankAccount",
                c => new
                    {
                        BankAccountId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 200),
                        DisplaySequence = c.Short(),
                        TypeOfBankAccountId = c.Int(nullable: false),
                        AccountId = c.Long(),
                        JobId = c.Int(),
                        BankAccountName = c.String(maxLength: 200),
                        BankAccountNumber = c.String(maxLength: 100),
                        RoutingNumber = c.String(maxLength: 100),
                        TypeOfCheckStockId = c.Int(),
                        LastCheckNumberGenerated = c.Long(),
                        ControlAccount = c.String(maxLength: 200),
                        ClearingAccountId = c.Long(),
                        ClearingJobId = c.Int(),
                        ExpirationMMYYYY = c.String(maxLength: 50),
                        TypeOfUploadFileId = c.Int(),
                        VendorId = c.Int(),
                        ControllingBankAccountId = c.Long(),
                        IsClosed = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        PositivePayTypeOfUploadFileId = c.Int(),
                        PositivePayTransmitterInfo = c.String(maxLength: 200),
                        PettyCashAccountId = c.Long(),
                        IsACHEnabled = c.Boolean(),
                        ACHDestinationCode = c.String(maxLength: 100),
                        ACHDestinationName = c.String(maxLength: 200),
                        ACHOriginCode = c.String(maxLength: 100),
                        ACHOriginName = c.String(maxLength: 200),
                        BatchId = c.Int(),
                        CCFullAccountNO = c.String(maxLength: 100),
                        CCFootNote = c.String(maxLength: 200),
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
                    { "DynamicFilter_BankAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BankAccountId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.AccountId)
                .ForeignKey("dbo.CAPS_Batch", t => t.BatchId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.ClearingAccountId)
                .ForeignKey("dbo.CAPS_Job", t => t.ClearingJobId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.ControllingBankAccountId)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.PettyCashAccountId)
                .ForeignKey("dbo.CAPS_TypeOfUploadFile", t => t.PositivePayTypeOfUploadFileId)
                .ForeignKey("dbo.CAPS_TypeOfCheckStock", t => t.TypeOfCheckStockId)
                .ForeignKey("dbo.CAPS_TypeOfUploadFile", t => t.TypeOfUploadFileId)
                .ForeignKey("dbo.CAPS_Vendors", t => t.VendorId)
                .Index(t => t.AccountId)
                .Index(t => t.JobId)
                .Index(t => t.TypeOfCheckStockId)
                .Index(t => t.ClearingAccountId)
                .Index(t => t.ClearingJobId)
                .Index(t => t.TypeOfUploadFileId)
                .Index(t => t.VendorId)
                .Index(t => t.ControllingBankAccountId)
                .Index(t => t.PositivePayTypeOfUploadFileId)
                .Index(t => t.PettyCashAccountId)
                .Index(t => t.BatchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BankAccount", "VendorId", "dbo.CAPS_Vendors");
            DropForeignKey("dbo.CAPS_BankAccount", "TypeOfUploadFileId", "dbo.CAPS_TypeOfUploadFile");
            DropForeignKey("dbo.CAPS_BankAccount", "TypeOfCheckStockId", "dbo.CAPS_TypeOfCheckStock");
            DropForeignKey("dbo.CAPS_BankAccount", "PositivePayTypeOfUploadFileId", "dbo.CAPS_TypeOfUploadFile");
            DropForeignKey("dbo.CAPS_BankAccount", "PettyCashAccountId", "dbo.CAPS_Accounts");
            DropForeignKey("dbo.CAPS_BankAccount", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_BankAccount", "ControllingBankAccountId", "dbo.CAPS_Accounts");
            DropForeignKey("dbo.CAPS_BankAccount", "ClearingJobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_BankAccount", "ClearingAccountId", "dbo.CAPS_Accounts");
            DropForeignKey("dbo.CAPS_BankAccount", "BatchId", "dbo.CAPS_Batch");
            DropForeignKey("dbo.CAPS_BankAccount", "AccountId", "dbo.CAPS_Accounts");
            DropIndex("dbo.CAPS_BankAccount", new[] { "BatchId" });
            DropIndex("dbo.CAPS_BankAccount", new[] { "PettyCashAccountId" });
            DropIndex("dbo.CAPS_BankAccount", new[] { "PositivePayTypeOfUploadFileId" });
            DropIndex("dbo.CAPS_BankAccount", new[] { "ControllingBankAccountId" });
            DropIndex("dbo.CAPS_BankAccount", new[] { "VendorId" });
            DropIndex("dbo.CAPS_BankAccount", new[] { "TypeOfUploadFileId" });
            DropIndex("dbo.CAPS_BankAccount", new[] { "ClearingJobId" });
            DropIndex("dbo.CAPS_BankAccount", new[] { "ClearingAccountId" });
            DropIndex("dbo.CAPS_BankAccount", new[] { "TypeOfCheckStockId" });
            DropIndex("dbo.CAPS_BankAccount", new[] { "JobId" });
            DropIndex("dbo.CAPS_BankAccount", new[] { "AccountId" });
            DropTable("dbo.CAPS_BankAccount",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BankAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
