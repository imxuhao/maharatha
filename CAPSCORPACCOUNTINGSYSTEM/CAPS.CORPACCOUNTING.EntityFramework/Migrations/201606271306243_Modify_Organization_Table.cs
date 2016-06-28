namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Organization_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_OrganizationUnits", "TransmitterContactName", c => c.String(maxLength: 1000));
            AddColumn("dbo.CAPS_OrganizationUnits", "TransmitterEmailAddress", c => c.String(maxLength: 1000));
            AddColumn("dbo.CAPS_OrganizationUnits", "TransmitterCode", c => c.String(maxLength: 1000));
            AddColumn("dbo.CAPS_OrganizationUnits", "TransmitterControlCode", c => c.String(maxLength: 1000));
            AddColumn("dbo.CAPS_OrganizationUnits", "FederalTaxId", c => c.String(maxLength: 15));
            AddColumn("dbo.CAPS_OrganizationUnits", "Logo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_OrganizationUnits", "Logo");
            DropColumn("dbo.CAPS_OrganizationUnits", "FederalTaxId");
            DropColumn("dbo.CAPS_OrganizationUnits", "TransmitterControlCode");
            DropColumn("dbo.CAPS_OrganizationUnits", "TransmitterCode");
            DropColumn("dbo.CAPS_OrganizationUnits", "TransmitterEmailAddress");
            DropColumn("dbo.CAPS_OrganizationUnits", "TransmitterContactName");
        }
    }
}
