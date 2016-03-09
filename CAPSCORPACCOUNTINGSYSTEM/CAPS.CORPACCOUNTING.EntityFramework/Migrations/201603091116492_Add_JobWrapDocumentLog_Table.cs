namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobWrapDocumentLog_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobWrapDocumentLog",
                c => new
                    {
                        JobWrapDocumentLogId = c.Int(nullable: false, identity: true),
                        JobBudgetId = c.Int(nullable: false),
                        TypeOfInvoiceId = c.Int(),
                        InvoiceInfo = c.String(maxLength: 50),
                        InvoiceDate = c.DateTime(storeType: "smalldatetime"),
                        TypeOfCurrencyId = c.Short(),
                        Payee = c.String(maxLength: 300),
                        Description = c.String(maxLength: 200),
                        CheckInfo = c.String(maxLength: 50),
                        CheckDate = c.DateTime(storeType: "smalldatetime"),
                        PoNumber = c.String(maxLength: 50),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        IsUploaded = c.Boolean(nullable: false),
                        DateImported = c.DateTime(storeType: "smalldatetime"),
                        ImportedByUserId = c.Int(),
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
                    { "DynamicFilter_JobWrapDocumentLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobWrapDocumentLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobWrapDocumentLogId)
                .ForeignKey("dbo.CAPS_JobBudget", t => t.JobBudgetId, cascadeDelete: true)
                .Index(t => t.JobBudgetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobWrapDocumentLog", "JobBudgetId", "dbo.CAPS_JobBudget");
            DropIndex("dbo.CAPS_JobWrapDocumentLog", new[] { "JobBudgetId" });
            DropTable("dbo.CAPS_JobWrapDocumentLog",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobWrapDocumentLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobWrapDocumentLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
