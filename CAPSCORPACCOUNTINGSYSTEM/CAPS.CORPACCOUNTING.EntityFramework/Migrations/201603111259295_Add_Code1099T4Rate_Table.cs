namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Code1099T4Rate_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Code1099T4Rate",
                c => new
                    {
                        Code1099T4RateId = c.Short(nullable: false, identity: true),
                        TypeOf1099T4Id = c.Int(nullable: false),
                        EffectiveDate = c.DateTime(storeType: "smalldatetime"),
                        BoxLocation = c.Short(),
                        Threshold = c.Decimal(precision: 18, scale: 2),
                        IsThresholdCombined = c.Boolean(nullable: false),
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
                    { "DynamicFilter_Code1099T4RateUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Code1099T4RateUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Code1099T4RateId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_Code1099T4Rate",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Code1099T4RateUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_Code1099T4RateUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
