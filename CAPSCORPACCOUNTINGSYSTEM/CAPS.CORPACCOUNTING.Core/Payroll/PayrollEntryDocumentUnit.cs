using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Payables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
namespace CAPS.CORPACCOUNTING.Payroll
{
    /// <summary>
    /// PayrollEntryDocument is the table name in lajit
    /// </summary>
    [Table("CAPS_PayrollEntryDocument")]
    public class PayrollEntryDocumentUnit : AccountingHeaderTransactionsUnit
    {
        /// <summary>Gets or sets the BatchId field. </summary>
        public virtual int? BatchId { get; set; }

        [ForeignKey("BatchId")]
        public virtual BatchUnit Batch { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public virtual int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the TypeOfInvoiceId field. </summary>
        [EnumDataType(typeof(TypeOfInvoice))]
        public virtual TypeOfInvoice TypeOfInvoiceId { get; set; }

        /// <summary>Gets or sets the IsEnterable field. </summary>
        public virtual bool IsEnterable { get; set; }

        /// <summary>Gets or sets the APInvoiceAccountingDocId field. </summary>
        public virtual long? APInvoiceAccountingDocId { get; set; }

        /// <summary>Gets or sets the UploadDocumentLogId field. </summary>
        public virtual int? UploadDocumentLogId { get; set; }

        /// <summary>Gets or sets the IsReversed field. </summary>
        public virtual bool? IsReversed { get; set; }

        /// <summary>Gets or sets the ReversedByUserId field. </summary>
        public virtual int? ReversedByUserId { get; set; }

        /// <summary>Gets or sets the ReversalDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? ReversalDate { get; set; }

        /// <summary>Gets or sets the IsVoid field. </summary>
        public virtual bool? IsVoid { get; set; }

        /// <summary>Gets or sets the IsVoidDateOriginal field. </summary>
        public virtual bool? IsVoidDateOriginal { get; set; }

        /// <summary>Gets or sets the LinkedAccountingDocumentId field. </summary>
        public virtual long? LinkedAccountingDocumentId { get; set; }

        public List<PayrollEntryDocumentDetailUnit> PayrollEntryDocumentDetailList;
        public PayrollEntryDocumentUnit() { }

        public PayrollEntryDocumentUnit(int? batchid, int? vendorid, TypeOfInvoice typeofinvoiceid, bool isenterable, long? apinvoiceaccountingdocid, int? uploaddocumentlogid,
                                                bool? isreversed, int? reversedbyuserid, DateTime? reversaldate, bool? isvoid, bool? isvoiddateoriginal, long? linkedaccountingdocumentid,
                                                 string description, TypeOfAccountingDocument typeofaccountingdocumentid, TypeofObject? typeofobjectid, long? recurdocid, long? reversedocid,
                                                 DateTime? documentdate, DateTime transactiondate, DateTime? dateposted, long? originaldocumentid, decimal? controltotal, string documentreference,
                                                 string voucherreference, short? typeofcurrencyid, int? currencyadjustmentid, string postbatchdescription, bool isposted, bool isautoposted, bool ischanged,
                                                 int? postedbyuserid, int? bankreccontrolid, bool isselected, bool isactive, bool isapproved, TypeOfInactiveStatus? typeofinactivestatusid,
                                                 bool? isbankrecomitted, bool? isictjournal, int? ictcompanyid, long? ictaccountingdocumentid, double? currencyoverriderate,
                                                 decimal? functionalcurrencycontroltotal, short? typeofcurrencyrateid, string memoline, bool? is13period, decimal? homecurrencyamount, decimal? customforexrate,
                                                 bool isposubmitforapproval, bool? iscpastran, int? cpasprojcloseid, int? cpasprojid, long organizationunitid) :
                             base(description: description, typeofaccountingdocumentid: typeofaccountingdocumentid, typeofobjectid: typeofobjectid, recurdocid: recurdocid,
                                                reversedocid: reversedocid, documentdate: documentdate, transactiondate: transactiondate, dateposted: dateposted,
                                                originaldocumentid: organizationunitid, controltotal: controltotal, documentreference: documentreference,
                                                voucherreference: voucherreference, typeofcurrencyid: typeofcurrencyid, currencyadjustmentid: currencyadjustmentid,
                                                postbatchdescription: postbatchdescription, isposted: isposted, isautoposted: isautoposted, ischanged: ischanged,
                                                postedbyuserid: postedbyuserid, bankreccontrolid: bankreccontrolid, isselected: isselected, isactive: isactive, isapproved: isapproved,
                                                typeofinactivestatusid: typeofinactivestatusid, isbankrecomitted: isbankrecomitted, isictjournal: isictjournal,
                                                ictcompanyid: ictcompanyid, ictaccountingdocumentid: ictaccountingdocumentid, currencyoverriderate: currencyoverriderate,
                                                functionalcurrencycontroltotal: functionalcurrencycontroltotal, typeofcurrencyrateid: typeofcurrencyid, memoline: memoline,
                                                is13period: is13period, homecurrencyamount: homecurrencyamount, customforexrate: customforexrate, isposubmitforapproval: isposubmitforapproval,
                                                iscpastran: iscpastran, cpasprojcloseid: cpasprojcloseid, cpasprojid: cpasprojid, organizationunitid: organizationunitid)
        {
            BatchId = batchid;
            VendorId = vendorid;
            TypeOfInvoiceId = typeofinvoiceid;
            IsEnterable = isenterable;
            APInvoiceAccountingDocId = apinvoiceaccountingdocid;
            UploadDocumentLogId = uploaddocumentlogid;
            IsReversed = isreversed;
            ReversedByUserId = reversedbyuserid;
            ReversalDate = reversaldate;
            IsVoid = isvoid;
            IsVoidDateOriginal = isvoiddateoriginal;
            LinkedAccountingDocumentId = linkedaccountingdocumentid;
        }

    }
}
