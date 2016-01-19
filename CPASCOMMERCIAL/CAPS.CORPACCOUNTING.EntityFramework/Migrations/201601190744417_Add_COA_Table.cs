namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_COA_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChartOfAccounts",
                c => new
                    {
                        COAId = c.Int(nullable: false, identity: true),
                        Caption = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 400),
                        ChartofAccountsType = c.Int(nullable: false),
                        DisplaySequence = c.Int(),
                        IsApproved = c.Boolean(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        TenantId = c.Int(nullable: false),
                        OrganizationUnitId = c.Long(),
                        IsActive = c.Boolean(nullable: false),
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
                    { "DynamicFilter_CoaUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CoaUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.COAId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ChartOfAccounts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CoaUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CoaUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
