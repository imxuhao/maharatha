namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ArStatementDetail_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ARStatementDetail",
                c => new
                    {
                        ARStatementDetailId = c.Long(nullable: false, identity: true),
                        AccountingDocumentId = c.Long(nullable: false),
                        SequenceNumber = c.Int(),
                        Description = c.String(),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        IsShaded = c.Boolean(nullable: false),
                        IsUnderlined = c.Boolean(nullable: false),
                        IsLargeFont = c.Boolean(nullable: false),
                        IsSmallFont = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Boolean(nullable: false),
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
                    { "DynamicFilter_ArStatementDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ArStatementDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ARStatementDetailId)
                .ForeignKey("dbo.CAPS_AccountingDocument", t => t.AccountingDocumentId, cascadeDelete: true)
                .Index(t => t.AccountingDocumentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ARStatementDetail", "AccountingDocumentId", "dbo.CAPS_AccountingDocument");
            DropIndex("dbo.CAPS_ARStatementDetail", new[] { "AccountingDocumentId" });
            DropTable("dbo.CAPS_ARStatementDetail",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ArStatementDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ArStatementDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
