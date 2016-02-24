namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Batch_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Batch",
                c => new
                    {
                        BatchId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        TypeOfBatchId = c.Int(nullable: false),
                        DefaultTransactionDate = c.DateTime(storeType: "smalldatetime"),
                        DefaultCheckDate = c.DateTime(storeType: "smalldatetime"),
                        PostingDate = c.DateTime(storeType: "smalldatetime"),
                        ControlTotal = c.Decimal(precision: 18, scale: 2),
                        RecurMonthIncrement = c.Int(),
                        IsRetained = c.Boolean(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        IsBatchFinalized = c.Boolean(),
                        IsUniversal = c.Boolean(),
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
                    { "DynamicFilter_BatchUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BatchUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BatchId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_Batch",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BatchUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BatchUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
