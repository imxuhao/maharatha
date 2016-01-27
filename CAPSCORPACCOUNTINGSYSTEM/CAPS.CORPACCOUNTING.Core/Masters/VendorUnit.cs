using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Masters
{
    public enum TypeofVendor
    {
        [Display(Name = "Standard")]Standard = 1,
        [Display(Name = "Credit Card")]CreditCard = 2,
        [Display(Name = "Petty Cash")]Pc = 3,
        [Display(Name = "Tax Agency")]TaxAgency = 4,
        [Display(Name = "Payroll Company")]PayrollCompany = 5,
        [Display(Name = "Shipping Partner")]ShippingPartner = 6,
        [Display(Name = "Per Diem Employee/Crew Member")]Pdecm = 7,
        [Display(Name = "Director ")]Director = 8,
        [Display(Name = "Sales Rep")]SalesRep = 9,
        [Display(Name = "Messenger")]Messenger = 10,
        [Display(Name = "Car Service")]CarService = 11,
        [Display(Name = "Phones")]Phones = 12
    }
    public enum Typeof1099T4
    {
        [Display(Name = "Box 1.Rents")]Rent = 1,
        [Display(Name = "Box 2.Royalties")]Royalty = 2,
        [Display(Name = "Box 3.Other Income")]OtherIncome = 3,
        [Display(Name = "Box 4.Federal Income Tax Withheld")]FedTxWthld = 4,
        [Display(Name = "Box 5.Fishing Boat Proceed")]Fishing = 5,
        [Display(Name = "Box 6.Medical/Healthcare Payments")]MedHealthcare = 6,
        [Display(Name = "Box 7.Non-Employee Compensation")]NonEmployee = 7,
        [Display(Name = "Box 8.Payments In Lieu Of Divdends/Interest")]DivInt  = 8,
        [Display(Name = "Box 9.Payer Made $5,000+ Direct Sales")] DirectSales = 9,
        [Display(Name = "Box 10.Crop Insurance Proceeds")]CropInsurance = 10,
        [Display(Name = "Box 13.Excess Golden Parachute Payment")]ExcessGldnPmnt = 11,
        [Display(Name = "Box 14.Gross Proceeds Paid To Attorney")] ProcdAttrnyPmnt = 12,
        [Display(Name = "(Canada) Rent")] CanadaRent = 13,
        [Display(Name = "(Canada) NEC/Attorney")] NecAttorney = 14,
    }
    [Table("Caps_Vendors")]
    public sealed class VendorUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        ///     Maximum length of the <see cref="MaxDisplayNameLength" /> property.
        /// </summary>
        public const int MaxDisplayNameLength = 100;

        public const int MaxAchLength = 20;
        public const int MaxCodeLength = 12;
        public const int MaxSsnLength = 15;
        public const int MaxRegionLength = 10;
        public const int MaxAchwireLength = 30;

        /// <summary>
        ///     Maximum size of Description.
        /// </summary>
        public const int MaxVendorLength = 50;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoaUnit" /> class  with no parameter.
        /// </summary>
        public VendorUnit()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoaUnit" /> class.
        /// </summary>
        public VendorUnit(string lastname, string firstname = null, string paytoname = null, string dbaname = null,
            string vendornumber = null, string vendoraccountinfo = null,
            string fedraltaxid = null, string ssntaxid = null, decimal? creditlimit = null, int? paymenttermsid = null,
            TypeofPaymentMethod typeofpaymentmethod = TypeofPaymentMethod.Check,
            string typeofcurrency = null, bool iscorporation = false, bool is1099 = false,
            bool isindependentcontractor = false, bool isw9Onfile = false, string achroutingnumber=null,
            TypeofVendor typeofvendorid = TypeofVendor.CarService,
            Typeof1099T4 typeof1099Box = Typeof1099T4.CanadaRent, DateTime? eddcontractstartdate = null,
            DateTime? eddcontractstopdate = null, decimal? eddconctractamount = null,
            string workregion = null, bool iseddcontractongoing = false, string achbankname = null,
            string achaccountnumber = null,
            string achwirefrombankname = null, string achwirefrombankaddress = null, string achwirefromswiftcode = null,
            string achwirefromaccountnumber = null, string achwiretobankname = null,
            string achwiretoswiftcode = null, string achwiretobeneficiary = null, string achwiretoaccountnumber = null,
            string achwiretoiban = null, bool isactive = true, bool isapproved = true, long? organizationunitid = null)
        {
            LastName = lastname;
            FirstName = firstname;
            PayToName = paytoname;
            DbaName = dbaname;
            FedralTaxId = fedraltaxid;
            VendorNumber = vendornumber;
            VendorAccountInfo = vendoraccountinfo;
            SSNTaxId = ssntaxid;
            CreditLimit = creditlimit;
            PaymentTermsId = paymenttermsid;
            TypeofCurrency = typeofcurrency;
            IsCorporation = iscorporation;
            Is1099 = is1099;
            IsIndependentContractor = isindependentcontractor;
            Isw9OnFile = isw9Onfile;
            TypeOFvendorId = typeofvendorid;
            TypeOF1099Box = typeof1099Box;
            EDDContractStartDate= eddcontractstartdate;
            EDDContractStopDate = eddcontractstopdate;
            EDDConctractAmount = eddconctractamount;
            WorkRegion = workregion;
            IsEDDContractOnGoing = iseddcontractongoing;
            ACHBankName = achbankname;
            ACHAccountNumber = achaccountnumber;
            ACHWireFromBankName = achwirefrombankname;
            ACHWireFromBankAddress = achwirefrombankaddress;
            ACHWireFromSwiftCode = achwirefromswiftcode;
            ACHWireFromAccountNumber = achwirefromaccountnumber;
            ACHWireToBankName = achwiretobankname;
            ACHWireToSwiftCode = achwiretoswiftcode;
            ACHWireToBeneficiary = achwiretobeneficiary;
            ACHWireToAccountNumber = achwiretoaccountnumber;
            ACHWireToIBAN = achwiretoiban;
            IsActive = isactive;
            IsApproved = isapproved;
            OrganizationUnitId = organizationunitid;
            ACHRoutingNumber = achroutingnumber;
        }

        #region Class Property Declarations

        /// <summary>Overriding the ID column with VendorID</summary>
        [Column("VendorId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the LastName field. </summary>
        [StringLength(MaxDisplayNameLength)]
        [Required]
        public string LastName { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        [StringLength(MaxDisplayNameLength)]
        public string FirstName { get; set; }

        /// <summary>Gets or sets the PayToName. </summary>
        [StringLength(MaxDisplayNameLength)]
        public string PayToName { get; set; }

        /// <summary>Gets or sets the DBAName field. </summary>

        [StringLength(MaxDisplayNameLength)]
        public string DbaName { get; set; }

        /// <summary>Gets or sets the IsPrivate field. </summary>
        [StringLength(MaxVendorLength)]
        public string VendorNumber { get; set; }
        /// <summary>Gets or sets the IsPrivate field. </summary>
        [StringLength(MaxDisplayNameLength)]
        public string VendorAccountInfo { get; set; }

        /// <summary>Gets or sets the FedralTaxId field. </summary>
        [StringLength(MaxSsnLength)]
        public string FedralTaxId { get; set; }

        /// <summary>Gets or sets the SSNTaxId field. </summary>
        [StringLength(MaxSsnLength)]
        public string SSNTaxId { get; set; }

        /// <summary>Gets or sets the CreditLimit field. </summary>
        public decimal? CreditLimit { get; set; }
       
        /// <summary>Gets or sets the TypeofPaymentMethod field. </summary>
        public TypeofPaymentMethod TypeofPaymentMethod { get; set; }

        [ForeignKey("PaymentTermsId")]
        public VendorPaymentTermUnit PaymentTerms { get; set; }

        /// <summary>Gets or sets the TypeofPaymentMethod field. </summary>
        public int? PaymentTermsId { get; set; }

        /// <summary>Gets or sets the TypeofCurrency field. </summary>
        [StringLength(MaxAchLength)]
        public string TypeofCurrency { get; set; }

        /// <summary>Gets or sets the IsCorporation field. </summary>
        public bool IsCorporation { get; set; }

        /// <summary>Gets or sets the Is1099 field. </summary>
        public bool Is1099 { get; set; }
        /// <summary>Gets or sets the IsIndependentContractor field. </summary>
        public bool IsIndependentContractor { get; set; }

        /// <summary>Gets or sets the Isw9OnFile field. </summary>
        public bool Isw9OnFile { get; set; }

        /// <summary>Gets or sets the TypeOFvendorId field. </summary>
        public TypeofVendor TypeOFvendorId { get; set; }

        /// <summary>Gets or sets the Typeof1099T4 field. </summary>
        public Typeof1099T4 TypeOF1099Box { get; set; }

        /// <summary>Gets or sets the EDDContractStartDate field. </summary>
        public DateTime? EDDContractStartDate { get; set; }

        /// <summary>Gets or sets the EDDContractStopDate field. </summary>
        public DateTime? EDDContractStopDate { get; set; }

        /// <summary>Gets or sets the EDDContractStopDate field. </summary>
        public decimal? EDDConctractAmount { get; set; }

        /// <summary>Gets or sets the WorkRegion field. </summary>
        [StringLength(MaxRegionLength)]
        public string WorkRegion { get; set; }

        /// <summary>Gets or sets the IsEDDContractOnGoing field. </summary>
        public bool IsEDDContractOnGoing { get; set; }
        /// <summary>Gets or sets the ACHBankName field. </summary>

        [StringLength(MaxDisplayNameLength)]
        public string ACHBankName { get; set; }
        /// <summary>Gets or sets the ACHRoutingNumber field. </summary>
        [StringLength(MaxAchLength)]
        public string ACHRoutingNumber { get; set; }

        [StringLength(MaxAchLength)]
        public string ACHAccountNumber { get; set; }

        /// <summary>Gets or sets the ACHWireFromBankName field. </summary>
        [StringLength(MaxDisplayNameLength)]
        public string ACHWireFromBankName { get; set; }

        /// <summary>Gets or sets the ACHWireFromBankName field. </summary>
        [StringLength(MaxDisplayNameLength)]
        public string ACHWireFromBankAddress { get; set; }

        /// <summary>Gets or sets the ACHWireFromSwiftCode field. </summary>
        [StringLength(MaxCodeLength)]
        public string ACHWireFromSwiftCode { get; set; }

        /// <summary>Gets or sets the ACHWireFromAccountNumber field. </summary>
        [StringLength(MaxCodeLength)]
        public string ACHWireFromAccountNumber { get; set; }

        /// <summary>Gets or sets the ACHWireToBankName field. </summary>
        [StringLength(MaxDisplayNameLength)]
        public string ACHWireToBankName { get; set; }

        /// <summary>Gets or sets the ACHWireToSwiftCode field. </summary>
        [StringLength(MaxCodeLength)]
        public string ACHWireToSwiftCode { get; set; }

        /// <summary>Gets or sets the ACHWireToBeneficiary field. </summary>
        [StringLength(MaxDisplayNameLength)]
        public string ACHWireToBeneficiary { get; set; }

        /// <summary>Gets or sets the ACHWireToAccountNumber field. </summary>
        [StringLength(MaxAchLength)]
        public string ACHWireToAccountNumber { get; set; }

        /// <summary>Gets or sets the ACHWireToIBAN field. </summary>
        [StringLength(MaxAchwireLength)]
        public string ACHWireToIBAN { get; set; }
       
        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }
       
        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; }


        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        
        #endregion
    }
}