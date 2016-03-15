namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CurrencyTypeOfRate_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_CurrencyTypeOfRate",
                c => new
                    {
                        CurrencyTypeOfRateId = c.Int(nullable: false, identity: true),
                        TypeOfCurrencyRateId = c.Short(nullable: false),
                        Description = c.String(),
                        IsDefault = c.Boolean(),
                        IsActive = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        OrganizationUnitId = c.Long(),
                        TenantId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CurrencyTypeOfRateUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.CurrencyTypeOfRateId)
                .ForeignKey("dbo.CAPS_TypeOfCurrencyRate", t => t.TypeOfCurrencyRateId, cascadeDelete: true)
                .Index(t => t.TypeOfCurrencyRateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_CurrencyTypeOfRate", "TypeOfCurrencyRateId", "dbo.CAPS_TypeOfCurrencyRate");
            DropIndex("dbo.CAPS_CurrencyTypeOfRate", new[] { "TypeOfCurrencyRateId" });
            DropTable("dbo.CAPS_CurrencyTypeOfRate",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CurrencyTypeOfRateUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
