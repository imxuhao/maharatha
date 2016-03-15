namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CurrencyType_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_CurrencyType",
                c => new
                    {
                        CurrencyTypeId = c.Int(nullable: false, identity: true),
                        TypeOfCurrencyId = c.Int(nullable: false),
                        GainAccountId = c.Int(),
                        GainJobId = c.Int(),
                        LossAccountId = c.Int(),
                        LossJobId = c.Int(),
                        EntityId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
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
                    { "DynamicFilter_CurrencyTypeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CurrencyTypeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.CurrencyTypeId)
                .ForeignKey("dbo.CAPS_Entity", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_CurrencyType", "EntityId", "dbo.CAPS_Entity");
            DropIndex("dbo.CAPS_CurrencyType", new[] { "EntityId" });
            DropTable("dbo.CAPS_CurrencyType",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CurrencyTypeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CurrencyTypeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
