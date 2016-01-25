namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Address_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Caps_Address",
                c => new
                    {
                        AddressId = c.Long(nullable: false, identity: true),
                        ObjectId = c.Int(nullable: false),
                        TypeofObjectId = c.Int(nullable: false),
                        AddressTypeId = c.Int(nullable: false),
                        ContactNumber = c.String(maxLength: 4000),
                        Line1 = c.String(maxLength: 4000),
                        Line2 = c.String(maxLength: 4000),
                        Line3 = c.String(maxLength: 4000),
                        Line4 = c.String(maxLength: 4000),
                        City = c.String(maxLength: 4000),
                        State = c.String(maxLength: 4000),
                        Country = c.String(maxLength: 4000),
                        PostalCode = c.String(maxLength: 4000),
                        Fax = c.String(maxLength: 4000),
                        Email = c.String(maxLength: 4000),
                        Phone1 = c.String(maxLength: 4000),
                        Phone1Extension = c.String(maxLength: 4000),
                        Phone2 = c.String(maxLength: 4000),
                        Phone2Extension = c.String(maxLength: 4000),
                        Website = c.String(maxLength: 1000),
                        IsPrimary = c.Boolean(nullable: false,defaultValue:true),
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
                    { "DynamicFilter_AddressUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AddressUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Caps_Address",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AddressUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AddressUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
