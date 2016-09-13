namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ProfitRule_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ProfitRule",
                c => new
                    {
                        ProfitRuleId = c.Int(nullable: false, identity: true),
                        TypeOfObjectId = c.Int(nullable: false),
                        ObjectId = c.Int(nullable: false),
                        Description = c.String(maxLength: 400),
                        DisplaySequence = c.Short(),
                        TypeOfProfitCalcId = c.Int(),
                        JobTypeId = c.Int(),
                        EffectiveDate = c.DateTime(),
                        ContractAniversaryDate = c.DateTime(),
                        FlatProfitAmount = c.Decimal(precision: 18, scale: 2),
                        DefaultOverHeadPercent = c.Decimal(precision: 18, scale: 2),
                        DefaultProfitPercent1 = c.Decimal(precision: 18, scale: 2),
                        DefaultProfitLimit1 = c.Decimal(precision: 18, scale: 2),
                        UnderBudgetPercent1 = c.Decimal(precision: 18, scale: 2),
                        DefaultProfitPercent2 = c.Decimal(precision: 18, scale: 2),
                        DefaultProfitLimit2 = c.Decimal(precision: 18, scale: 2),
                        UnderBudgetPercent2 = c.Decimal(precision: 18, scale: 2),
                        DefaultProfitPercent3 = c.Decimal(precision: 18, scale: 2),
                        DefaultProfitLimit3 = c.Decimal(precision: 18, scale: 2),
                        UnderBudgetPercent3 = c.Decimal(precision: 18, scale: 2),
                        DefaultProfitPercent4 = c.Decimal(precision: 18, scale: 2),
                        DefaultProfitLimit4 = c.Decimal(precision: 18, scale: 2),
                        UnderBudgetPercent4 = c.Decimal(precision: 18, scale: 2),
                        ProdExpTypeOfAmountId = c.Short(),
                        ProdRevTypeOfAmountId = c.Short(),
                        AtoKOvrTypeOfAmountId = c.Short(),
                        ProdFeeOvrTypeOfAmountId = c.Short(),
                        ProdFeeTypeOfAmountId = c.Short(),
                        DirectorFeeTypeOfAmountId = c.Short(),
                        AtoKTypeOfAmountId = c.Short(),
                        AtoKExpense = c.Decimal(precision: 18, scale: 2),
                        AtoKOverage = c.Decimal(precision: 18, scale: 2),
                        ProductionFee = c.Decimal(precision: 18, scale: 2),
                        ProductionFeeOverage = c.Decimal(precision: 18, scale: 2),
                        ProductionExpense = c.Decimal(precision: 18, scale: 2),
                        ProductionRevenue = c.Decimal(precision: 18, scale: 2),
                        DirectorFee = c.Decimal(precision: 18, scale: 2),
                        OverheadExpense = c.Decimal(precision: 18, scale: 2),
                        OverheadTypeOfAmountId = c.Short(),
                        ProfitThresholdPercent = c.Decimal(precision: 18, scale: 2),
                        ProfitTypeOfAmountId = c.Short(),
                        IsNonProfitBypassed = c.Boolean(nullable: false),
                        SubAccountId = c.Long(),
                        VendorId = c.Int(),
                        TypeOfFrequencyId = c.Int(),
                        EntityId = c.Int(),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeofInactiveStatusId = c.Int(),
                        TenantId = c.Int(nullable: false),
                        OrganizationUnitId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        SubAccountUnit_Id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ProfitRuleUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ProfitRuleUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ProfitRuleId)
                .ForeignKey("dbo.CAPS_Entity", t => t.EntityId)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountUnit_Id)
                .ForeignKey("dbo.CAPS_TypeOfProfitCalc", t => t.TypeOfProfitCalcId)
                .ForeignKey("dbo.CAPS_Vendor", t => t.VendorId)
                .Index(t => t.TypeOfProfitCalcId)
                .Index(t => t.VendorId)
                .Index(t => t.EntityId)
                .Index(t => t.SubAccountUnit_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ProfitRule", "VendorId", "dbo.CAPS_Vendor");
            DropForeignKey("dbo.CAPS_ProfitRule", "TypeOfProfitCalcId", "dbo.CAPS_TypeOfProfitCalc");
            DropForeignKey("dbo.CAPS_ProfitRule", "SubAccountUnit_Id", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_ProfitRule", "EntityId", "dbo.CAPS_Entity");
            DropIndex("dbo.CAPS_ProfitRule", new[] { "SubAccountUnit_Id" });
            DropIndex("dbo.CAPS_ProfitRule", new[] { "EntityId" });
            DropIndex("dbo.CAPS_ProfitRule", new[] { "VendorId" });
            DropIndex("dbo.CAPS_ProfitRule", new[] { "TypeOfProfitCalcId" });
            DropTable("dbo.CAPS_ProfitRule",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ProfitRuleUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ProfitRuleUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
