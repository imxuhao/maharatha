namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_VendorPaymentTerm_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_VendorPaymentTerms",
                c => new
                    {
                        PaymentTermsId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        DueDays = c.Int(nullable: false),
                        DiscountDays = c.Int(),
                        IsActive = c.Boolean(nullable: false, defaultValue: true ),
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
                    { "DynamicFilter_VendorPaymentTermUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VendorPaymentTermUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.PaymentTermsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Caps_VendorPaymentTerms",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VendorPaymentTermUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VendorPaymentTermUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
