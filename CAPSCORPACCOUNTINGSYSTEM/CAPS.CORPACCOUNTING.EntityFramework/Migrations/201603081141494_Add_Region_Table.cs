namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Region_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Region",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 200),
                        RegionAbbreviation = c.String(maxLength: 10),
                        TypeOfCountryId = c.Short(),
                        StateCode = c.String(maxLength: 2),
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
                    { "DynamicFilter_RegionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RegionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.RegionId)
                .ForeignKey("dbo.CAPS_TypeOfCountry", t => t.TypeOfCountryId)
                .Index(t => t.TypeOfCountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_Region", "TypeOfCountryId", "dbo.CAPS_TypeOfCountry");
            DropIndex("dbo.CAPS_Region", new[] { "TypeOfCountryId" });
            DropTable("dbo.CAPS_Region",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RegionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RegionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
