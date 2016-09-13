namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ARYTDInvoice_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ARYTDInvoice",
                c => new
                    {
                        ARYTDInvoiceId = c.Long(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        ArInvoiceRequestId = c.Int(),
                        InvoiceDate = c.DateTime(storeType: "smalldatetime"),
                        InvoiceNumber = c.String(maxLength: 50),
                        TypeOfCurrencyId = c.Short(),
                        IsInvoicePrintRequired = c.Boolean(),
                        IsInvoiceHistory = c.Boolean(nullable: false),
                        IsEnterable = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        ManualAcctDocId = c.Long(),
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
                    { "DynamicFilter_ArytdInvoiceUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ArytdInvoiceUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ARYTDInvoiceId)
                .ForeignKey("dbo.CAPS_Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ARYTDInvoice", "CustomerId", "dbo.CAPS_Customer");
            DropIndex("dbo.CAPS_ARYTDInvoice", new[] { "CustomerId" });
            DropTable("dbo.CAPS_ARYTDInvoice",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ArytdInvoiceUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ArytdInvoiceUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
