namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PayrollEntryDocumentDetail_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_PayrollEntryDocumentDetail",
                c => new
                    {
                        AccountingItemId = c.Long(nullable: false),
                        ValueAddedTaxRecoveryId = c.Int(),
                        TaxGroupId = c.Int(),
                        Quantity = c.Decimal(precision: 18, scale: 2),
                        UnitPrice = c.Decimal(storeType: "money"),
                        Weight = c.Decimal(precision: 18, scale: 2),
                        VendorId = c.Int(),
                        EmployeeId = c.Int(),
                        EmployeeTaxNumber = c.String(),
                        TypeOfPayrollId = c.Short(),
                        PayrollTrxInfo = c.String(),
                        PayrollWorkHours = c.Decimal(precision: 18, scale: 2),
                        PayrollDays = c.Decimal(precision: 18, scale: 2),
                        PayrollRate = c.Decimal(storeType: "money"),
                        PayrollCheckNumber = c.String(),
                        PayrollCheckDate = c.DateTime(storeType: "smalldatetime"),
                        PayrollWorkDate = c.DateTime(storeType: "smalldatetime"),
                        PayrollOt = c.Decimal(precision: 18, scale: 2),
                        PayrollFica = c.Decimal(precision: 18, scale: 2),
                        PayrollFui = c.Decimal(precision: 18, scale: 2),
                        PayrollSui = c.Decimal(precision: 18, scale: 2),
                        PayrollWc = c.Decimal(precision: 18, scale: 2),
                        PayrollHf = c.Decimal(precision: 18, scale: 2),
                        PayrollPhw = c.Decimal(precision: 18, scale: 2),
                        PayrollJob = c.String(),
                        PayrollAccount = c.String(),
                        PayrollWeekEnding = c.DateTime(storeType: "smalldatetime"),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PayrollEntryDocumentDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PayrollEntryDocumentDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccountingItemId)
                .ForeignKey("dbo.CAPS_AccountingItem", t => t.AccountingItemId)
                .Index(t => t.AccountingItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_PayrollEntryDocumentDetail", "AccountingItemId", "dbo.CAPS_AccountingItem");
            DropIndex("dbo.CAPS_PayrollEntryDocumentDetail", new[] { "AccountingItemId" });
            DropTable("dbo.CAPS_PayrollEntryDocumentDetail",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PayrollEntryDocumentDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PayrollEntryDocumentDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
