using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Banking;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{
    public class DefaultTypeofUploadFileCreator
    {
        public static List<TypeOfUploadFileUnit> InitialTypeOfUploadFileList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;

        public DefaultTypeofUploadFileCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;

            InitialTypeOfUploadFileList = new List<TypeOfUploadFileUnit>
            {
              new TypeOfUploadFileUnit(description:"City National Bank",displaysequence:1,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:TyeofUpload.UploadMethod),
              new TypeOfUploadFileUnit(description:"HSBC",displaysequence:1,notes:null,uploadfilename:"HSBC Bank Rec Uploadfile.This also includes ACH Deposits",uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:TyeofUpload.UploadMethod),
              new TypeOfUploadFileUnit(description:"Bank Of America (CSV)",displaysequence:350,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:TyeofUpload.UploadMethod),
              new TypeOfUploadFileUnit(description:"The Bank Of California",displaysequence:90,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:TyeofUpload.UploadMethod),
              new TypeOfUploadFileUnit(description:"City National Bank (Positive Pay-121808)",displaysequence:1,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:TyeofUpload.PositivePayFile),
              new TypeOfUploadFileUnit(description:"City National Bank (Positive Pay-Payee CNZ/SFTP)",displaysequence:2,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:TyeofUpload.PositivePayFile),
              new TypeOfUploadFileUnit(description:"HSBC (Positive Pay-Standard 300)",displaysequence:3,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:true,uploadoptiond:false,overridejobid:null,typeofuploadid:TyeofUpload.PositivePayFile),
              new TypeOfUploadFileUnit(description:"JP Morgan Chase Bank (Positive Pay)",displaysequence:4,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:TyeofUpload.PositivePayFile),
              new TypeOfUploadFileUnit(description:"Bank of America (Positive Pay)",displaysequence:5,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:TyeofUpload.PositivePayFile),
               new TypeOfUploadFileUnit(description:"AMEX (Personal)",displaysequence:1,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"AMEX (KR1022)",displaysequence:1,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"AMEX (Personal)",displaysequence:2,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"AMEX (Centurion)",displaysequence:3,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"AMEX (Platinum)",displaysequence:4,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"AMEX (Plum)",displaysequence:5,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"AMEX (Platinum2)",displaysequence:6,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"Visa",displaysequence:6,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"Visa (CNB)",displaysequence:6,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"Visa (Chase lnk)",displaysequence:6,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"Master Card",displaysequence:6,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"MasterCard (BankofAmerica)",displaysequence:7,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"MasterCard (ChaseInk)",displaysequence:7,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"MasterCard (HSBC)",displaysequence:7,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"MasterCard (Citibank)",displaysequence:7,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
               new TypeOfUploadFileUnit(description:"Visa (CMasterCard (Barclays)",displaysequence:7,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null),
                new TypeOfUploadFileUnit(description:"MasterCard (CapitalOne)",displaysequence:7,notes:null,uploadfilename:null,uploadoptiona:false,uploadoptionb:false,uploadoptionc:false,uploadoptiond:false,overridejobid:null,typeofuploadid:null)
            };
        }
        public void Create()
        {
            CreateTypeOfUploadFileList();
        }

        private void CreateTypeOfUploadFileList()
        {
            foreach (var TypeOfUploadFile in InitialTypeOfUploadFileList)
            {
                AddTypeOfUploadFileListIfNotExists(TypeOfUploadFile);
            }
        }

        private void AddTypeOfUploadFileListIfNotExists(TypeOfUploadFileUnit typeOfUploadFile)
        {
            if (_context.TypeOfUploadFileUnits.Any(l => l.Description == typeOfUploadFile.Description))
            {
                return;
            }

            _context.TypeOfUploadFileUnits.Add(typeOfUploadFile);

            _context.SaveChanges();
        }

    }
}



