using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace CAPS.CORPACCOUNTING.Journals
{

    /// <summary>
    /// JournalEntryDocument is the table name in lajit
    /// </summary>
    [Table("CAPS_JournalEntryDocument")]    
    public class JournalEntryDocumentUnit : AccountingHeaderTransactionsUnit
    {
        ///<summary>Get Sets the BatchId field.</summary>
        public virtual int? BatchId { get; set; }

        [ForeignKey("BatchId")]
        public virtual BatchUnit Batch { get; set; }

        ///<summary>Get Sets the IsReversingEntry field.</summary>
        public virtual bool IsReversingEntry { get; set; }

        ///<summary>Get Sets the DateOfReversal field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? DateOfReversal { get; set; }

        ///<summary>Get Sets the IsRecurringEntry field.</summary>
        public virtual bool IsRecurringEntry { get; set; }

        ///<summary>Get Sets the DateToRecur field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? DateToRecur { get; set; }

        ///<summary>Get Sets the FinalDate field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? FinalDate { get; set; }

        ///<summary>Get Sets the LastPostDate field.</summary>
        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? LastPostDate { get; set; }

        ///<summary>Get Sets the BatchInfo field.</summary>
        public virtual string BatchInfo { get; set; }

        ///<summary>Get Sets the IsBatchRemoved field.</summary>
        public virtual bool? IsBatchRemoved { get; set; }

        public JournalEntryDocumentUnit() { }

        public JournalEntryDocumentUnit(int? batchid, bool isreversingentry, SqlDateTime? dateofreversal, bool isrecurringentry, SqlDateTime? datetorecur, SqlDateTime? finaldate,
                                        SqlDateTime? lastpostdate, string batchinfo, bool? isbatchremoved, string description, TypeOfAccountingDocument typeofaccountingdocumentid, TypeofObject? typeofobjectid,
                                        long? recurdocid, long? reversedocid, DateTime? documentdate, DateTime transactiondate, DateTime? dateposted, long? originaldocumentid, decimal? controltotal, string documentreference,
                                         string voucherreference, short? typeofcurrencyid, int? currencyadjustmentid, string postbatchdescription, bool isposted, bool isautoposted, bool ischanged,
                                         int? postedbyuserid, int? bankreccontrolid, bool isselected, bool isactive, bool isapproved, TypeOfInactiveStatus? typeofinactivestatusid,
                                         bool? isbankrecomitted, bool? isictjournal, int? ictcompanyid, long? ictaccountingdocumentid, double? currencyoverriderate,
                                         decimal? functionalcurrencycontroltotal, short? typeofcurrencyrateid, string memoline, bool? is13period, decimal? homecurrencyamount, decimal? customforexrate,
                                         bool isposubmitforapproval, bool? iscpastran, int? cpasprojcloseid, int? cpasprojid, long organizationunitid)
            : base(description: description, typeofaccountingdocumentid: typeofaccountingdocumentid, typeofobjectid: typeofobjectid, recurdocid: recurdocid, reversedocid: reversedocid, documentdate: documentdate,
                                        transactiondate: transactiondate, dateposted: dateposted, originaldocumentid: organizationunitid, controltotal: controltotal, documentreference: documentreference,
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
            IsReversingEntry = isreversingentry;
            DateOfReversal = dateofreversal;
            IsRecurringEntry = isrecurringentry;
            DateToRecur = datetorecur;
            FinalDate = finaldate;
            LastPostDate = lastpostdate;
            BatchInfo = batchinfo;
            IsBatchRemoved = isbatchremoved;
        }
    }
}
