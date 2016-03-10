namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ValueAddedTaxType_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ValueAddedTaxType",
                c => new
                    {
                        ValueAddedTaxTypeId = c.Int(nullable: false, identity: true),
                        TypeOfCountryId = c.Short(nullable: false),
                        TypeOfValueAddedTaxId = c.Int(nullable: false),
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
                    { "DynamicFilter_ValueAddedTaxTypeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ValueAddedTaxTypeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ValueAddedTaxTypeId)
                .ForeignKey("dbo.CAPS_TypeOfCountry", t => t.TypeOfCountryId, cascadeDelete: true)
                .Index(t => t.TypeOfCountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ValueAddedTaxType", "TypeOfCountryId", "dbo.CAPS_TypeOfCountry");
            DropIndex("dbo.CAPS_ValueAddedTaxType", new[] { "TypeOfCountryId" });
            DropTable("dbo.CAPS_ValueAddedTaxType",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ValueAddedTaxTypeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ValueAddedTaxTypeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
