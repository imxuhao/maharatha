namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Country_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Country",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        TypeOfCountryId = c.Short(),
                        TenantId = c.Int(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CountryUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.CountryID)
                .ForeignKey("dbo.CAPS_TypeOfCountry", t => t.TypeOfCountryId)
                .Index(t => t.TypeOfCountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_Country", "TypeOfCountryId", "dbo.CAPS_TypeOfCountry");
            DropIndex("dbo.CAPS_Country", new[] { "TypeOfCountryId" });
            DropTable("dbo.CAPS_Country",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CountryUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
