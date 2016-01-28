namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Customer_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(maxLength: 100),
                        CustomerNumber = c.String(maxLength: 50),
                        CreditLimit = c.Decimal(precision: 18, scale: 2),
                        TypeofPaymentMethodId = c.Int(nullable: false),
                        CustomerPayTermsId = c.Int(),
                        SalesRepId = c.Int(),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
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
                    { "DynamicFilter_CustomerUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CustomerUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.Caps_CustomerPaymentTerms", t => t.CustomerPayTermsId)
                .ForeignKey("dbo.Caps_SalesRep", t => t.SalesRepId)
                .Index(t => t.CustomerPayTermsId)
                .Index(t => t.SalesRepId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Caps_Customers", "SalesRepId", "dbo.Caps_SalesRep");
            DropForeignKey("dbo.Caps_Customers", "CustomerPayTermsId", "dbo.Caps_CustomerPaymentTerms");
            DropIndex("dbo.Caps_Customers", new[] { "SalesRepId" });
            DropIndex("dbo.Caps_Customers", new[] { "CustomerPayTermsId" });
            DropTable("dbo.Caps_Customers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CustomerUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CustomerUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
