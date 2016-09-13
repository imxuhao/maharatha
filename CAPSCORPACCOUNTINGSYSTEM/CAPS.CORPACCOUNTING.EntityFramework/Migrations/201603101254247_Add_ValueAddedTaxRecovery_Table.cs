namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ValueAddedTaxRecovery_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ValueAddedTaxRecovery",
                c => new
                    {
                        ValueAddedTaxRecoveryId = c.Int(nullable: false, identity: true),
                        ValueAddedTaxTypeId = c.Int(nullable: false),
                        TypeOfVatRecoveryId = c.Int(nullable: false),
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
                    { "DynamicFilter_ValueAddedTaxRecoveryUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ValueAddedTaxRecoveryUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ValueAddedTaxRecoveryId)
                .ForeignKey("dbo.CAPS_ValueAddedTaxType", t => t.ValueAddedTaxTypeId, cascadeDelete: true)
                .Index(t => t.ValueAddedTaxTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ValueAddedTaxRecovery", "ValueAddedTaxTypeId", "dbo.CAPS_ValueAddedTaxType");
            DropIndex("dbo.CAPS_ValueAddedTaxRecovery", new[] { "ValueAddedTaxTypeId" });
            DropTable("dbo.CAPS_ValueAddedTaxRecovery",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ValueAddedTaxRecoveryUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ValueAddedTaxRecoveryUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
