namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_WorkSheet1099_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_WorkSheet1099",
                c => new
                    {
                        WorkSheet1099Id = c.Long(nullable: false, identity: true),
                        VendorId = c.Int(),
                        TaxYear = c.Int(),
                        TypeOf1099T4Id = c.Int(),
                        TypeOfCountryId = c.Short(),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        IsSelectedToPrint = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        IsPrimaryAddress = c.Boolean(nullable: false),
                        SsnTaxId = c.String(),
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
                    { "DynamicFilter_WorkSheet1099Unit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkSheet1099Unit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.WorkSheet1099Id)
                .ForeignKey("dbo.CAPS_TypeOfCountry", t => t.TypeOfCountryId)
                .ForeignKey("dbo.CAPS_Vendor", t => t.VendorId)
                .Index(t => t.VendorId)
                .Index(t => t.TypeOfCountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_WorkSheet1099", "VendorId", "dbo.CAPS_Vendor");
            DropForeignKey("dbo.CAPS_WorkSheet1099", "TypeOfCountryId", "dbo.CAPS_TypeOfCountry");
            DropIndex("dbo.CAPS_WorkSheet1099", new[] { "TypeOfCountryId" });
            DropIndex("dbo.CAPS_WorkSheet1099", new[] { "VendorId" });
            DropTable("dbo.CAPS_WorkSheet1099",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WorkSheet1099Unit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkSheet1099Unit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
