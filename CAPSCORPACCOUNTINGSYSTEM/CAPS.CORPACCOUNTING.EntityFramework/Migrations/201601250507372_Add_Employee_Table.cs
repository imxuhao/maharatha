namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Employee_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Employee",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(maxLength: 100),
                        EmployeeRegion = c.String(maxLength: 10),
                        SSNTaxId = c.String(maxLength: 15),
                        FederalTaxId = c.String(maxLength: 15),
                        Is1099 = c.Boolean(nullable: false),
                        IsW9OnFile = c.Boolean(nullable: false),
                        IsIndependantContractor = c.Boolean(nullable: false),
                        IsCorporation = c.Boolean(nullable: false),
                        IsProducer = c.Boolean(nullable: false),
                        IsDirector = c.Boolean(nullable: false),
                        IsDirPhoto = c.Boolean(nullable: false),
                        IsSetDesigner = c.Boolean(nullable: false),
                        IsEditor = c.Boolean(nullable: false),
                        IsArtDirector = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false,defaultValue:true),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
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
                    { "DynamicFilter_EmployeeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EmployeeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Caps_Employee",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EmployeeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EmployeeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
