namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AccountingControl_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_AccountingControl",
                c => new
                    {
                        AccountingControlId = c.Int(nullable: false, identity: true),
                        TypeOfAccountingControlId = c.Int(nullable: false),
                        ControlValue = c.String(),
                        ControlCount = c.Int(),
                        EntityId = c.Int(),
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
                    { "DynamicFilter_AccountingControlUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountingControlUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccountingControlId)
                .ForeignKey("dbo.CAPS_Entity", t => t.EntityId)
                .Index(t => t.EntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_AccountingControl", "EntityId", "dbo.CAPS_Entity");
            DropIndex("dbo.CAPS_AccountingControl", new[] { "EntityId" });
            DropTable("dbo.CAPS_AccountingControl",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AccountingControlUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountingControlUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
