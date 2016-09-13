namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfGenericProcess_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfGenericProcess",
                c => new
                    {
                        TypeOfGenericProcessId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        Choose1 = c.Int(),
                        Choose2 = c.Int(),
                        Choose3 = c.Int(),
                        Choose4 = c.Int(),
                        Choose5 = c.Int(),
                        StartDate1 = c.DateTime(storeType: "smalldatetime"),
                        EndDate1 = c.DateTime(storeType: "smalldatetime"),
                        StartDate2 = c.DateTime(storeType: "smalldatetime"),
                        EndDate2 = c.DateTime(storeType: "smalldatetime"),
                        StartDate3 = c.DateTime(storeType: "smalldatetime"),
                        EndDate3 = c.DateTime(storeType: "smalldatetime"),
                        StartRange1 = c.String(maxLength: 50),
                        EndRange1 = c.String(maxLength: 50),
                        StartRange2 = c.String(maxLength: 50),
                        EndRange2 = c.String(maxLength: 50),
                        StartRange3 = c.String(maxLength: 50),
                        EndRange3 = c.String(maxLength: 50),
                        Select1 = c.Boolean(),
                        Select2 = c.Boolean(),
                        Select3 = c.Boolean(),
                        Select4 = c.Boolean(),
                        Select5 = c.Boolean(),
                        Choose6 = c.Int(),
                        Choose7 = c.Int(),
                        Choose8 = c.Int(),
                        Choose9 = c.Int(),
                        Choose10 = c.Int(),
                        StartDate4 = c.DateTime(storeType: "smalldatetime"),
                        EndDate4 = c.DateTime(storeType: "smalldatetime"),
                        StartDate5 = c.DateTime(storeType: "smalldatetime"),
                        EndDate5 = c.DateTime(storeType: "smalldatetime"),
                        StartDate6 = c.DateTime(storeType: "smalldatetime"),
                        EndDate6 = c.DateTime(storeType: "smalldatetime"),
                        StartDate7 = c.DateTime(storeType: "smalldatetime"),
                        EndDate7 = c.DateTime(storeType: "smalldatetime"),
                        StartDate8 = c.DateTime(storeType: "smalldatetime"),
                        EndDate8 = c.DateTime(storeType: "smalldatetime"),
                        StartDate9 = c.DateTime(storeType: "smalldatetime"),
                        EndDate9 = c.DateTime(storeType: "smalldatetime"),
                        StartDate10 = c.DateTime(storeType: "smalldatetime"),
                        EndDate10 = c.DateTime(storeType: "smalldatetime"),
                        StartRange4 = c.String(maxLength: 50),
                        EndRange4 = c.String(maxLength: 50),
                        StartRange5 = c.String(maxLength: 50),
                        EndRange5 = c.String(maxLength: 50),
                        StartRange6 = c.String(maxLength: 50),
                        EndRange6 = c.String(maxLength: 50),
                        StartRange7 = c.String(maxLength: 50),
                        EndRange7 = c.String(maxLength: 50),
                        StartRange8 = c.String(maxLength: 50),
                        EndRange8 = c.String(maxLength: 50),
                        StartRange9 = c.String(maxLength: 50),
                        EndRange9 = c.String(maxLength: 50),
                        StartRange10 = c.String(maxLength: 50),
                        EndRange10 = c.String(maxLength: 50),
                        Select6 = c.Boolean(),
                        Select7 = c.Boolean(),
                        Select8 = c.Boolean(),
                        Select9 = c.Boolean(),
                        Select10 = c.Boolean(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfGenericProcessId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfGenericProcess");
        }
    }
}
