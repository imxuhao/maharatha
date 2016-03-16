namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CurrencyRate_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_CurrencyRate",
                c => new
                    {
                        CurrencyRateId = c.Int(nullable: false, identity: true),
                        TypeOfCurrencyId = c.Int(nullable: false),
                        EffectiveDate = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        CurrencyRate = c.Double(nullable: false),
                        TypeOfCurrencyRateId = c.Short(),
                        EffectiveHr = c.Short(),
                        EffectiveMin = c.Short(),
                        FromTypeOfCurrencyId = c.Int(),
                        TenantId = c.Int(nullable: false),
                        OrganizationUnitId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CurrencyRateUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.CurrencyRateId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_CurrencyRate",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CurrencyRateUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
