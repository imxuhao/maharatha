namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_WebAddress_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_WebAddress",
                c => new
                    {
                        WebAddressId = c.Int(nullable: false, identity: true),
                        TypeOfCategoryId = c.Short(nullable: false),
                        TypeOfWebAddressId = c.Int(nullable: false),
                        TypeOfObjectId = c.Int(nullable: false),
                        ObjectId = c.Int(nullable: false),
                        ContactInfo = c.String(),
                        UrlAddress = c.String(),
                        IsPrimary = c.Boolean(nullable: false),
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
                    { "DynamicFilter_WebAddressUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WebAddressUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.WebAddressId)
                .ForeignKey("dbo.CAPS_TypeOfCategory", t => t.TypeOfCategoryId, cascadeDelete: true)
                .Index(t => t.TypeOfCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_WebAddress", "TypeOfCategoryId", "dbo.CAPS_TypeOfCategory");
            DropIndex("dbo.CAPS_WebAddress", new[] { "TypeOfCategoryId" });
            DropTable("dbo.CAPS_WebAddress",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WebAddressUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WebAddressUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
