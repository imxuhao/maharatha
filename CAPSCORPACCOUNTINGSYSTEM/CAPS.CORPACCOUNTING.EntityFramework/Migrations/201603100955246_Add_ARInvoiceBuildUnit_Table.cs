namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ARInvoiceBuildUnit_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ARInvoiceBuild",
                c => new
                    {
                        ARInvoiceBuildID = c.Long(nullable: false, identity: true),
                        TypeOfArInvoiceBuildId = c.Short(),
                        Description = c.String(),
                        DisplaySequence = c.Short(),
                        CalcPercent = c.Decimal(precision: 18, scale: 2),
                        IsLastCalc = c.Boolean(nullable: false),
                        ArBillingTypeId = c.Int(),
                        ArPaymentTermId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        EntityId = c.Int(nullable: false),
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
                    { "DynamicFilter_ArInvoiceBuildUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ArInvoiceBuildUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ARInvoiceBuildID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_ARInvoiceBuild",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ArInvoiceBuildUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ArInvoiceBuildUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
