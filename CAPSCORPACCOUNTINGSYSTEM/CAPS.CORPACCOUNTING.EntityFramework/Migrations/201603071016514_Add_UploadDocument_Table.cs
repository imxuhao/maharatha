namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UploadDocument_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_UploadDocumentLog",
                c => new
                    {
                        UploadDocumentLogId = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        TypeOfAccountingDocumentId = c.Int(nullable: false),
                        TypeOfUploadFileId = c.Int(),
                        InvoiceInfo = c.String(),
                        InvoiceDate = c.DateTime(),
                        TypeOfCurrencyId = c.Short(),
                        ControlAmount1 = c.Decimal(precision: 18, scale: 2),
                        ControlAmount2 = c.Decimal(precision: 18, scale: 2),
                        ControlAmount3 = c.Decimal(precision: 18, scale: 2),
                        ControlAmount4 = c.Decimal(precision: 18, scale: 2),
                        ControlAmount5 = c.Decimal(precision: 18, scale: 2),
                        DateImported = c.DateTime(),
                        ImportedByUserId = c.Int(),
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
                    { "DynamicFilter_UploadDocumentLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UploadDocumentLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.UploadDocumentLogId)
                .ForeignKey("dbo.CAPS_TypeOfUploadFile", t => t.TypeOfUploadFileId)
                .Index(t => t.TypeOfUploadFileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_UploadDocumentLog", "TypeOfUploadFileId", "dbo.CAPS_TypeOfUploadFile");
            DropIndex("dbo.CAPS_UploadDocumentLog", new[] { "TypeOfUploadFileId" });
            DropTable("dbo.CAPS_UploadDocumentLog",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UploadDocumentLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UploadDocumentLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
