namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_TransmitterrelatedFieldsfrom_organization : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CAPS_OrganizationUnits", "TransmitterContactName");
            DropColumn("dbo.CAPS_OrganizationUnits", "TransmitterEmailAddress");
            DropColumn("dbo.CAPS_OrganizationUnits", "TransmitterCode");
            DropColumn("dbo.CAPS_OrganizationUnits", "TransmitterControlCode");
            DropColumn("dbo.CAPS_OrganizationUnits", "FederalTaxId");
            DropColumn("dbo.CAPS_OrganizationUnits", "Logo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_OrganizationUnits", "Logo", c => c.Binary());
            AddColumn("dbo.CAPS_OrganizationUnits", "FederalTaxId", c => c.String(maxLength: 15));
            AddColumn("dbo.CAPS_OrganizationUnits", "TransmitterControlCode", c => c.String(maxLength: 1000));
            AddColumn("dbo.CAPS_OrganizationUnits", "TransmitterCode", c => c.String(maxLength: 1000));
            AddColumn("dbo.CAPS_OrganizationUnits", "TransmitterEmailAddress", c => c.String(maxLength: 1000));
            AddColumn("dbo.CAPS_OrganizationUnits", "TransmitterContactName", c => c.String(maxLength: 1000));
        }
    }
}
