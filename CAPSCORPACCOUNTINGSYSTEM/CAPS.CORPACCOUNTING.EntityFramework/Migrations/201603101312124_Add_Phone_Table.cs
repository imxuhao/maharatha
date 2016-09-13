namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Phone_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Phone",
                c => new
                    {
                        PhoneId = c.Int(nullable: false, identity: true),
                        TypeOfCategoryId = c.Short(nullable: false),
                        TypeOfPhoneId = c.Int(nullable: false),
                        TypeOfObjectId = c.Int(nullable: false),
                        ObjectId = c.Int(nullable: false),
                        ContactInfo = c.String(),
                        TelephoneNumber = c.String(maxLength: 50),
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
                    { "DynamicFilter_PhoneUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PhoneUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.PhoneId)
                .ForeignKey("dbo.CAPS_TypeOfCategory", t => t.TypeOfCategoryId, cascadeDelete: true)
                .Index(t => t.TypeOfCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_Phone", "TypeOfCategoryId", "dbo.CAPS_TypeOfCategory");
            DropIndex("dbo.CAPS_Phone", new[] { "TypeOfCategoryId" });
            DropTable("dbo.CAPS_Phone",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PhoneUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PhoneUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
