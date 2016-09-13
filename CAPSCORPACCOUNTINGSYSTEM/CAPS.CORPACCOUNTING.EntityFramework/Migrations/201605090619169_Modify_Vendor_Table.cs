namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Vendor_Table : DbMigration
    {
        public override void Up()
        {

            Sql(@"IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_CAPS_Vendor_TypeofPaymentMethod]') AND type in (N'F'))
                BEGIN
                ALTER TABLE dbo.CAPS_Vendor DROP CONSTRAINT[FK_CAPS_Vendor_TypeofPaymentMethod]
                END");
            Sql(@"IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_CAPS_Vendor_Typeof1099Box]') AND type in (N'F'))
                BEGIN
                ALTER TABLE dbo.CAPS_Vendor DROP CONSTRAINT[FK_CAPS_Vendor_Typeof1099Box]
                END");
            AddColumn("dbo.CAPS_Vendor", "TypeofPaymentMethodId", c => c.Int());
            AddColumn("dbo.CAPS_Vendor", "TypeofCurrencyId", c => c.Int(nullable: false));
            AddColumn("dbo.CAPS_Vendor", "Typeof1099BoxId", c => c.Int());
            AddColumn("dbo.CAPS_Vendor", "BillingAccount", c => c.String(maxLength: 100));
            AddColumn("dbo.CAPS_Vendor", "TypeofTaxId", c => c.Int());
            AddColumn("dbo.CAPS_Vendor", "TaxCreditId", c => c.Int());
            AddColumn("dbo.CAPS_Vendor", "JobId", c => c.Int());
            AddColumn("dbo.CAPS_Vendor", "GLAccountId", c => c.Long());
            AddColumn("dbo.CAPS_Vendor", "AccountId", c => c.Long());
            AddColumn("dbo.CAPS_Vendor", "Notes", c => c.String());
            DropColumn("dbo.CAPS_Vendor", "TypeofPaymentMethod");
            DropColumn("dbo.CAPS_Vendor", "TypeofCurrency");
            DropColumn("dbo.CAPS_Vendor", "Typeof1099Box");
        }
        
        public override void Down()
        {

            Sql(@"IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_CAPS_Vendor_TypeofTaxId]') AND type in (N'F'))
                BEGIN
                ALTER TABLE dbo.CAPS_Vendor DROP CONSTRAINT[FK_CAPS_Vendor_TypeofTaxId]
                END");
            Sql(@"IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_CAPS_Vendor_TypeofTaxId]') AND type in (N'F'))
                BEGIN
                ALTER TABLE dbo.CAPS_Vendor DROP CONSTRAINT[FK_CAPS_Vendor_TypeofTaxId]
                END");
            Sql(@"IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_CAPS_Vendor_Typeof1099BoxId]') AND type in (N'F'))
                BEGIN
                ALTER TABLE dbo.CAPS_Vendor DROP CONSTRAINT[FK_CAPS_Vendor_Typeof1099BoxId]
                END");
            Sql(@"IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_CAPS_Vendor_TypeofPaymentMethodId]') AND type in (N'F'))
                BEGIN
                ALTER TABLE dbo.CAPS_Vendor DROP CONSTRAINT[FK_CAPS_Vendor_TypeofPaymentMethodId]
                END");
            AddColumn("dbo.CAPS_Vendor", "Typeof1099Box", c => c.Int());
            AddColumn("dbo.CAPS_Vendor", "TypeofCurrency", c => c.String(maxLength: 20));
            AddColumn("dbo.CAPS_Vendor", "TypeofPaymentMethod", c => c.Int());
            DropColumn("dbo.CAPS_Vendor", "Notes");
            DropColumn("dbo.CAPS_Vendor", "AccountId");
            DropColumn("dbo.CAPS_Vendor", "GLAccountId");
            DropColumn("dbo.CAPS_Vendor", "JobId");
            DropColumn("dbo.CAPS_Vendor", "TaxCreditId");
            DropColumn("dbo.CAPS_Vendor", "TypeofTaxId");
            DropColumn("dbo.CAPS_Vendor", "BillingAccount");
            DropColumn("dbo.CAPS_Vendor", "Typeof1099BoxId");
            DropColumn("dbo.CAPS_Vendor", "TypeofCurrencyId");
            DropColumn("dbo.CAPS_Vendor", "TypeofPaymentMethodId");
        }
    }
}
