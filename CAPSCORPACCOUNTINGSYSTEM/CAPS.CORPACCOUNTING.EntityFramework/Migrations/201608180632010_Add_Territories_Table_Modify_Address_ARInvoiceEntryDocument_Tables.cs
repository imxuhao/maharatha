namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Territories_Table_Modify_Address_ARInvoiceEntryDocument_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Territories",
                c => new
                    {
                        TerritorieId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
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
                    { "DynamicFilter_TerritoriesUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TerritoriesUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.TerritorieId);
            
            AddColumn("dbo.CAPS_ARInvoiceEntryDocument", "IsStartUp", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_Address", "TerritorieId", c => c.Int());
            CreateIndex("dbo.CAPS_Address", "TerritorieId");
            AddForeignKey("dbo.CAPS_Address", "TerritorieId", "dbo.CAPS_Territories", "TerritorieId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_Address", "TerritorieId", "dbo.CAPS_Territories");
            DropIndex("dbo.CAPS_Address", new[] { "TerritorieId" });
            DropColumn("dbo.CAPS_Address", "TerritorieId");
            DropColumn("dbo.CAPS_ARInvoiceEntryDocument", "IsStartUp");
            DropTable("dbo.CAPS_Territories",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TerritoriesUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TerritoriesUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
