namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BankRecClearedUnit_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BankRecCleared",
                c => new
                    {
                        BankRecClearedId = c.Int(nullable: false, identity: true),
                        BankRecControlId = c.Int(nullable: false),
                        TypeOfAccountingDocumentId = c.Int(),
                        AccountingDocumentId = c.Long(),
                        UploadTrxType = c.String(maxLength: 50),
                        UploadDate = c.DateTime(),
                        UploadNumber = c.String(maxLength: 50),
                        UploadInfo = c.String(maxLength: 400),
                        UploadAmount = c.Decimal(precision: 18, scale: 2),
                        IsCleared = c.Boolean(nullable: false),
                        AccountingItemId = c.Long(),
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
                    { "DynamicFilter_BankRecClearedUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankRecClearedUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BankRecClearedId)
                .ForeignKey("dbo.CAPS_AccountingDocument", t => t.AccountingDocumentId)
                .ForeignKey("dbo.CAPS_AccountingItem", t => t.AccountingItemId)
                .Index(t => t.AccountingDocumentId)
                .Index(t => t.AccountingItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BankRecCleared", "AccountingItemId", "dbo.CAPS_AccountingItem");
            DropForeignKey("dbo.CAPS_BankRecCleared", "AccountingDocumentId", "dbo.CAPS_AccountingDocument");
            DropIndex("dbo.CAPS_BankRecCleared", new[] { "AccountingItemId" });
            DropIndex("dbo.CAPS_BankRecCleared", new[] { "AccountingDocumentId" });
            DropTable("dbo.CAPS_BankRecCleared",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BankRecClearedUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankRecClearedUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
