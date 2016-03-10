namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PayrollControlFringe_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_PayrollControlFringe",
                c => new
                    {
                        PayrollControlFringeId = c.Int(nullable: false, identity: true),
                        PayrollControlId = c.Int(nullable: false),
                        TypeOfPayrollFringeId = c.Int(nullable: false),
                        StartingRange = c.String(nullable: false),
                        EndingRange = c.String(nullable: false),
                        SelectStartRange = c.String(),
                        SelectEndRange = c.String(),
                        FringeAccountMask = c.String(),
                        SubAccountId1 = c.Long(),
                        SubAccountId2 = c.Long(),
                        SubAccountId3 = c.Long(),
                        SubAccountId4 = c.Long(),
                        SubAccountId5 = c.Long(),
                        SubAccountId6 = c.Long(),
                        SubAccountId7 = c.Long(),
                        SubAccountId8 = c.Long(),
                        SubAccountId9 = c.Long(),
                        SubAccountId10 = c.Long(),
                        IsCorporateDivisonRequired = c.Boolean(),
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
                    { "DynamicFilter_PayrollControlFringeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PayrollControlFringeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.PayrollControlFringeId)
                .ForeignKey("dbo.CAPS_PayrollControl", t => t.PayrollControlId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId1)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId10)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId2)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId3)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId4)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId5)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId6)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId7)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId8)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId9)
                .Index(t => t.PayrollControlId)
                .Index(t => t.SubAccountId1)
                .Index(t => t.SubAccountId2)
                .Index(t => t.SubAccountId3)
                .Index(t => t.SubAccountId4)
                .Index(t => t.SubAccountId5)
                .Index(t => t.SubAccountId6)
                .Index(t => t.SubAccountId7)
                .Index(t => t.SubAccountId8)
                .Index(t => t.SubAccountId9)
                .Index(t => t.SubAccountId10);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_PayrollControlFringe", "SubAccountId9", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PayrollControlFringe", "SubAccountId8", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PayrollControlFringe", "SubAccountId7", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PayrollControlFringe", "SubAccountId6", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PayrollControlFringe", "SubAccountId5", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PayrollControlFringe", "SubAccountId4", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PayrollControlFringe", "SubAccountId3", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PayrollControlFringe", "SubAccountId2", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PayrollControlFringe", "SubAccountId10", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PayrollControlFringe", "SubAccountId1", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PayrollControlFringe", "PayrollControlId", "dbo.CAPS_PayrollControl");
            DropIndex("dbo.CAPS_PayrollControlFringe", new[] { "SubAccountId10" });
            DropIndex("dbo.CAPS_PayrollControlFringe", new[] { "SubAccountId9" });
            DropIndex("dbo.CAPS_PayrollControlFringe", new[] { "SubAccountId8" });
            DropIndex("dbo.CAPS_PayrollControlFringe", new[] { "SubAccountId7" });
            DropIndex("dbo.CAPS_PayrollControlFringe", new[] { "SubAccountId6" });
            DropIndex("dbo.CAPS_PayrollControlFringe", new[] { "SubAccountId5" });
            DropIndex("dbo.CAPS_PayrollControlFringe", new[] { "SubAccountId4" });
            DropIndex("dbo.CAPS_PayrollControlFringe", new[] { "SubAccountId3" });
            DropIndex("dbo.CAPS_PayrollControlFringe", new[] { "SubAccountId2" });
            DropIndex("dbo.CAPS_PayrollControlFringe", new[] { "SubAccountId1" });
            DropIndex("dbo.CAPS_PayrollControlFringe", new[] { "PayrollControlId" });
            DropTable("dbo.CAPS_PayrollControlFringe",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PayrollControlFringeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PayrollControlFringeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
