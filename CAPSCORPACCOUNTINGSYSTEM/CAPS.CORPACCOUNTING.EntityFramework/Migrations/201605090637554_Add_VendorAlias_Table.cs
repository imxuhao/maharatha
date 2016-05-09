namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_VendorAlias_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_VendorAlias",
                c => new
                    {
                        VendorAliasId = c.Int(nullable: false, identity: true),
                        VendorId = c.Int(nullable: false),
                        AliasName = c.String(),
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
                    { "DynamicFilter_VendorAliasUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.VendorAliasId)
                .ForeignKey("dbo.CAPS_Vendor", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_VendorAlias", "VendorId", "dbo.CAPS_Vendor");
            DropIndex("dbo.CAPS_VendorAlias", new[] { "VendorId" });
            DropTable("dbo.CAPS_VendorAlias",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VendorAliasUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
