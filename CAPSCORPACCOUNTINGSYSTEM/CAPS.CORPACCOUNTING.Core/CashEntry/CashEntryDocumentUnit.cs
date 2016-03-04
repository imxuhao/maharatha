using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace CAPS.CORPACCOUNTING.CashEntry
{
    [Table("CAPS_CashEntryDocument")]
    public class CashEntryDocumentUnit : AccountingHeaderTransactionsUnit
    {

        ///<summary>Get Sets the BatchId field.</summary>
        public virtual int? BatchId { get; set; }

        [ForeignKey("BatchId")]
        public virtual BatchUnit Batch { get; set; }

        ///<summary>Get Sets the BankAccountId field.</summary>
        public virtual int BankAccountId { get; set; }
        [ForeignKey("BankAccountId")]
        public virtual BankAccountUnit BankAccount { get; set; }

        ///<summary>Get Sets the ReconciliationId field.</summary>
        public virtual int? ReconciliationId { get; set; }

        ///<summary>Get Sets the IsEnterable field.</summary>
        public virtual bool IsEnterable { get; set; }

        ///<summary>Get Sets the SendingBankAccountId field.</summary>
        public virtual int? SendingBankAccountId { get; set; }

        [ForeignKey("SendingBankAccountId")]
        public virtual BankAccountUnit SendingBankAccount { get; set; }

        ///<summary>Get Sets the PettyCashAccountId field.</summary>
        public virtual long PettyCashAccountId { get; set; }

        [ForeignKey("PettyCashAccountId")]
        public AccountUnit PettyCashAccount { get; set; }

        ///<summary>Get Sets the BatchInfo field.</summary>
        public virtual string BatchInfo { get; set; }

        ///<summary>Get Sets the IsReversed field.</summary>
        public virtual bool? IsReversed { get; set; }

        ///<summary>Get Sets the ReversedByUserId field.</summary>
        public virtual int? ReversedByUserId { get; set; }

        ///<summary>Get Sets the ReversalDate field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? ReversalDate { get; set; }

        ///<summary>Get Sets the IsVoid field.</summary>
        public virtual bool? IsVoid { get; set; }

        ///<summary>Get Sets the IsVoidDateOriginal field.</summary>
        public virtual bool? IsVoidDateOriginal { get; set; }

        ///<summary>Get Sets the LinkedAccountingDocumentId field.</summary>
        public virtual long? LinkedAccountingDocumentId { get; set; }

        ///<summary>Get Sets the ReissueBatchId field.</summary>
        public virtual int? ReissueBatchId { get; set; }
        [ForeignKey("ReissueBatchId")]
        public virtual BatchUnit ReissueBatch { get; set; }

        ///<summary>Get Sets the ReissueVoidDate field.</summary>
        public virtual int? ReissueVoidDate { get; set; }

        ///<summary>Get Sets the DepositTypeOfCategoryId field.</summary>
        public virtual int? DepositTypeOfCategoryId { get; set; }

        public CashEntryDocumentUnit()
        {
        }


        public CashEntryDocumentUnit(int? batchid, int bankaccountid, int? reconciliationid, bool isenterable, int? sendingbankaccountid, long pettycashaccountid,
                                string batchinfo, bool? isreversed, int? reversedbyuserid, SqlDateTime? reversaldate, bool? isvoid, bool? isvoiddateoriginal,
                                long? linkedaccountingdocumentid, int? reissuebatchid, int? reissuevoiddate, int? deposittypeofcategoryid,
                                 string description, TypeOfAccountingDocument typeofaccountingdocumentid, TypeofObject? typeofobjectid, long? recurdocid, long? reversedocid,
                                DateTime? documentdate, DateTime transactiondate, DateTime? dateposted, long? originaldocumentid, decimal? controltotal, string documentreference,
                                string voucherreference, short? typeofcurrencyid, int? currencyadjustmentid, string postbatchdescription, bool isposted, bool isautoposted, bool ischanged,
                                int? postedbyuserid, int? bankreccontrolid, bool isselected, bool isactive, bool isapproved, TypeOfInactiveStatus? typeofinactivestatusid,
                                bool? isbankrecomitted, bool? isictjournal, int? ictcompanyid, long? ictaccountingdocumentid, double? currencyoverriderate,
                                decimal? functionalcurrencycontroltotal, short? typeofcurrencyrateid, string memoline, bool? is13period, decimal? homecurrencyamount, decimal? customforexrate,
                                bool isposubmitforapproval, bool? iscpastran, int? cpasprojcloseid, int? cpasprojid, long organizationunitid)
              : base(description: description, typeofaccountingdocumentid: typeofaccountingdocumentid, typeofobjectid: typeofobjectid, recurdocid: recurdocid,
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
            BankAccountId = bankaccountid;
            ReconciliationId = reconciliationid;
            IsEnterable = isenterable;
            SendingBankAccountId = sendingbankaccountid;
            PettyCashAccountId = pettycashaccountid;
            BatchInfo = batchinfo;
            IsReversed = isreversed;
            ReversedByUserId = reversedbyuserid;
            ReversalDate = reversaldate;
            IsVoid = isvoid;
            IsVoidDateOriginal = isvoiddateoriginal;
            LinkedAccountingDocumentId = linkedaccountingdocumentid;
            ReissueBatchId = reissuebatchid;
            ReissueVoidDate = reissuevoiddate;
            DepositTypeOfCategoryId = deposittypeofcategoryid;

        }
    }
}
