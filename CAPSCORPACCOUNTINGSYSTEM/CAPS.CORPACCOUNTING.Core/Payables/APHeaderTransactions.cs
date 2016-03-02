using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System.Data.SqlTypes;

namespace CAPS.CORPACCOUNTING.Payables
{
    public enum TypeOfInvoice
    {
        [Display(Name = "Invoice")]
        Invoice = 1,
        [Display(Name = "Credit Memo")]
        CreditMemo = 2,
        [Display(Name = "Void Payment Only")]
        VoidPaymentOnly = 3,
        [Display(Name = "Void Pay & All Invs")]
        VoidPayandAllInvs = 4,
        [Display(Name = "Reissue Only")]
        ReissueOnly = 5,
        [Display(Name = "Reissue Reversal")]
        ReissueReversal = 6,
        [Display(Name = "Petty Cash")]
        PettyCash = 7,
        [Display(Name = "Payroll")]
        Payroll = 8,
        [Display(Name = "Credit Card")]
        CreditCard = 9,
        [Display(Name = "Debit Card")]
        DebitCard = 10,
        [Display(Name = "Shipping")]
        Shipping = 11,
        [Display(Name = "Check Log")]
        CheckLog = 12,
        [Display(Name = "PO Log")]
        POLog = 13,
        [Display(Name = "Petty Cash Log")]
        PettyCashLog = 14,
        [Display(Name = "Payroll Log")]
        PayrollLog = 15,
        [Display(Name = "Sales Log")]
        SalesLog = 16,
        [Display(Name = "AMEX Log")]
        AMEXLog = 17
    }
    public enum TypeOfCheckGroup
    {
        [Display(Name = "Group A")]
        GroupA = 1,
        [Display(Name = "Group B")]
        GroupB = 2,
        [Display(Name = "Group C")]
        GroupC = 3,
        [Display(Name = "Group D")]
        GroupD = 4,
        [Display(Name = "Group E")]
        GroupE = 5,
        [Display(Name = "Group F")]
        GroupF = 6,
        [Display(Name = "Separate Check")]
        SeparateCheck = 7,
        [Display(Name = "Group G")]
        GroupG = 8,
        [Display(Name = "Group H")]
        GroupH = 9,
        [Display(Name = "Group I")]
        GroupI = 10,
        [Display(Name = "Group J")]
        GroupJ = 11,
        [Display(Name = "Group K")]
        GroupK = 12,
        [Display(Name = "Group L")]
        GroupL = 13,
        [Display(Name = "Group M")]
        GroupM = 14,
        [Display(Name = "Group N")]
        GroupN = 15,
        [Display(Name = "Group O")]
        GroupO = 16,
        [Display(Name = "Group P")]
        GroupP = 17,
        [Display(Name = "Group Q")]
        GroupQ = 18,
        [Display(Name = "Group R")]
        GroupR = 19,
        [Display(Name = "Group S")]
        GroupS = 20,
        [Display(Name = "Group T")]
        GroupT = 21,
        [Display(Name = "Group U")]
        GroupU = 22,
        [Display(Name = "Group V")]
        GroupV = 23,
        [Display(Name = "Group W")]
        GroupW = 24,
        [Display(Name = "Group X")]
        GroupX = 25,
        [Display(Name = "Group Y")]
        GroupY = 26,
        [Display(Name = "Group Z")]
        GroupZ = 27

    }


    [Table("CAPS_APHeaderTransactions")]
    public class ApHeaderTransactions : AccountingHeaderTransactionsUnit
    {
        #region Class Property Declarations

        ///<summary>Get Sets the Posting Date</summary>
        // public virtual DateTime  CheckDate { get; set; }

        ///<summary>Get Sets the BatchId field.</summary>
        public virtual int? BatchId { get; set; }

        [ForeignKey("BatchId")]
        public virtual BatchUnit Batch { get; set; }

        ///<summary>Get Sets the VendorId field.</summary>
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        ///<summary>Get Sets the TypeOfInvoiceId field.</summary>
        [EnumDataType(typeof(TypeOfInvoice))]
        public virtual TypeOfInvoice TypeOfInvoiceId { get; set; }

        ///<summary>Get Sets the PettyCashAccountId field.</summary>
        public virtual long? PettyCashAccountId { get; set; }

        [ForeignKey("PettyCashAccountId")]
        public virtual AccountUnit Account { get; set; }


        ///<summary>Get Sets the PaymentTermId field.</summary>
        public virtual int? PaymentTermId { get; set; }

        [ForeignKey("PaymentTermId")]
        public virtual VendorPaymentTermUnit VendorPaymentTerms { get; set; }


        ///<summary>Get Sets the TypeOfCheckGroupId field.</summary>
        public virtual TypeOfCheckGroup? TypeOfCheckGroupId { get; set; }

        ///<summary>Get Sets the BankAccountId field.</summary>
        public virtual int? BankAccountId { get; set; }

        ///<summary>Get Sets the PaymentDate field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? PaymentDate { get; set; }

        ///<summary>Get Sets the PaymentNumber field.</summary>
        public virtual string PaymentNumber { get; set; }

        ///<summary>Get Sets the PurchaseOrderReference field.</summary>
        [StringLength(100)]
        public virtual string PurchaseOrderReference { get; set; }

        ///<summary>Get Sets the ReversedByUserId field.</summary>
        public virtual int? ReversedByUserId { get; set; }

        ///<summary>Get Sets the ReversalDate field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? ReversalDate { get; set; }

        ///<summary>Get Sets the IsInvoiceHistory field.</summary>
        public virtual bool IsInvoiceHistory { get; set; }

        ///<summary>Get Sets the IsEnterable field.</summary>
        public virtual bool IsEnterable { get; set; }

        ///<summary>Get Sets the GeneratedAccountingDocumentId field.</summary>
        public virtual long? GeneratedAccountingDocumentId { get; set; }

        ///<summary>Get Sets the UploadDocumentLogID field.</summary>
        public virtual int? UploadDocumentLogID { get; set; }

        ///<summary>Get Sets the BatchInfo field.</summary>
        public virtual string BatchInfo { get; set; }

        ///<summary>Get Sets the PaymentSelectedByUserId field.</summary>
        public virtual int? PaymentSelectedByUserId { get; set; }


        #endregion

        public ApHeaderTransactions() { }
        public ApHeaderTransactions(int? batchid, int? vendorid, TypeOfInvoice typeofinvoiceid, long? pettycashaccountid, int? paymenttermid, TypeOfCheckGroup? typeofcheckgroupid, 
            int? bankaccountid, DateTime? paymentdate, string paymentnumber, string purchaseorderreference, int? reversedbyuserid, DateTime? reversaldate, bool isinvoicehistory,
            bool isenterable, long? generatedaccountingdocumentid, int? uploaddocumentlogid, string batchinfo, int? paymentselectedbyuserid,
            string description, TypeOfAccountingDocument typeofaccountingdocumentid, TypeofObject? typeofobjectid, long? recurdocid, long? reversedocid,
                 DateTime? documentdate, DateTime transactiondate, DateTime? dateposted, long? originaldocumentid, decimal? controltotal, string documentreference,
                 string voucherreference, short? typeofcurrencyid, int? currencyadjustmentid, string postbatchdescription, bool isposted, bool isautoposted, bool ischanged,
                 int? postedbyuserid, int? bankreccontrolid, bool isselected, bool isactive, bool isapproved, TypeOfInactiveStatus? typeofinactivestatusid,
                 bool? isbankrecomitted, bool? isictjournal, int? ictcompanyid, long? ictaccountingdocumentid, double? currencyoverriderate,
                 decimal? functionalcurrencycontroltotal, short? typeofcurrencyrateid, string memoline, bool? is13period, decimal? homecurrencyamount, decimal? customforexrate,
                 bool isposubmitforapproval, bool? iscpastran, int? cpasprojcloseid, int? cpasprojid, long organizationunitid) :base(description:description,typeofaccountingdocumentid:typeofaccountingdocumentid)
        {
            BatchId = batchid;
            VendorId = vendorid;
            TypeOfInvoiceId = typeofinvoiceid;
            PettyCashAccountId = pettycashaccountid;
            PaymentTermId = paymenttermid;
            TypeOfCheckGroupId = typeofcheckgroupid;
            BankAccountId = bankaccountid;
            PaymentDate = paymentdate;
            PaymentNumber = paymentnumber;
            PurchaseOrderReference = purchaseorderreference;
            ReversedByUserId = reversedbyuserid;
            ReversalDate = reversaldate;
            IsInvoiceHistory = isinvoicehistory;
            IsEnterable = isenterable;
            GeneratedAccountingDocumentId = generatedaccountingdocumentid;
            UploadDocumentLogID = uploaddocumentlogid;
            BatchInfo = batchinfo;
            PaymentSelectedByUserId = paymentselectedbyuserid;

        }



    }
}



