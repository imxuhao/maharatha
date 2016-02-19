namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Director_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Director",
                c => new
                    {
                        DirectorId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        SSNTaxId = c.String(maxLength: 15),
                        FederalTaxId = c.String(maxLength: 15),
                        BusinessManager = c.String(maxLength: 500),
                        PayToName = c.String(maxLength: 500),
                        Is1099 = c.Boolean(),
                        Typeof1099Id = c.Int(),
                        ContractDate = c.DateTime(),
                        ExpirationDate = c.DateTime(),
                        Fee = c.Decimal(storeType: "money"),
                        ContractTypeId = c.Int(),
                        ProfitShareTermId = c.Int(),
                        Comments = c.String(maxLength: 500),
                        OrganizationUnitId = c.Long(),
                        TenantId = c.Int(nullable: false),
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
                    { "DynamicFilter_DirectorUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DirectorUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.DirectorId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_Director",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DirectorUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DirectorUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
