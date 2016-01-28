namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CustomerPaymentTerm_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_CustomerPaymentTerms",
                c => new
                    {
                        CustomerPayTemrsId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 500),
                        PaymentInstruction = c.String(maxLength: 4000),
                        DueDays = c.Int(nullable: false),
                        DiscountPercent = c.Decimal(precision: 18, scale: 2),
                        DiscountDays = c.Int(),
                        OvernightInstructions = c.String(maxLength: 4000),
                        WiringInstructions = c.String(maxLength: 4000),
                        FooterMessage = c.String(maxLength: 4000),
                        LogoCaption = c.String(maxLength: 4000),
                        IsDefault = c.Boolean(nullable: true),
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
                    { "DynamicFilter_CustomerPaymentTermUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CustomerPaymentTermUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.CustomerPayTemrsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Caps_CustomerPaymentTerms",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CustomerPaymentTermUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CustomerPaymentTermUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
