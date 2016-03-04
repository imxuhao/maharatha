using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System.Data.SqlTypes;

namespace CAPS.CORPACCOUNTING.Payables
{


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
                 bool isposubmitforapproval, bool? iscpastran, int? cpasprojcloseid, int? cpasprojid, long organizationunitid) :
            base(description:description,typeofaccountingdocumentid:typeofaccountingdocumentid,typeofobjectid:typeofobjectid,recurdocid:recurdocid,
                reversedocid:reversedocid,documentdate:documentdate,transactiondate:transactiondate,dateposted:dateposted,
                originaldocumentid:organizationunitid,controltotal:controltotal,documentreference:documentreference,
                voucherreference:voucherreference,typeofcurrencyid:typeofcurrencyid,currencyadjustmentid:currencyadjustmentid,
                postbatchdescription:postbatchdescription,isposted:isposted,isautoposted:isautoposted,ischanged:ischanged,
                postedbyuserid:postedbyuserid,bankreccontrolid:bankreccontrolid,isselected:isselected,isactive:isactive,isapproved:isapproved,
                typeofinactivestatusid:typeofinactivestatusid,isbankrecomitted:isbankrecomitted,isictjournal:isictjournal,
                ictcompanyid:ictcompanyid,ictaccountingdocumentid:ictaccountingdocumentid,currencyoverriderate:currencyoverriderate,
                functionalcurrencycontroltotal:functionalcurrencycontroltotal,typeofcurrencyrateid:typeofcurrencyid,memoline:memoline,
                is13period:is13period,homecurrencyamount:homecurrencyamount,customforexrate:customforexrate,isposubmitforapproval:isposubmitforapproval,
                iscpastran:iscpastran, cpasprojcloseid:cpasprojcloseid, cpasprojid:cpasprojid,organizationunitid:organizationunitid)
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



