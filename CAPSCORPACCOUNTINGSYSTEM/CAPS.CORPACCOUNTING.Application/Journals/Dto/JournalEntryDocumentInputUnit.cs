using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Journals.dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(JournalEntryDocumentUnit))]
    [Serializable]
    public class JournalEntryDocumentInputUnit : AccountingHeaderTransactionInputUnit
    {
        ///<summary>Get Sets the BatchId field.</summary>
        public virtual int? BatchId { get; set; }

        ///<summary>Get Sets the IsReversingEntry field.</summary>
        public virtual bool IsReversingEntry { get; set; }

        ///<summary>Get Sets the DateOfReversal field.</summary>
        public virtual DateTime? DateOfReversal { get; set; }

        ///<summary>Get Sets the IsRecurringEntry field.</summary>
        public virtual bool IsRecurringEntry { get; set; }

        ///<summary>Get Sets the DateToRecur field.</summary>
        public virtual DateTime? DateToRecur { get; set; }

        ///<summary>Get Sets the FinalDate field.</summary>
        public virtual DateTime? FinalDate { get; set; }

        ///<summary>Get Sets the LastPostDate field.</summary>
        public virtual DateTime? LastPostDate { get; set; }

        ///<summary>Get Sets the BatchInfo field.</summary>
        public virtual string BatchInfo { get; set; }

        ///<summary>Get Sets the IsBatchRemoved field.</summary>
        public virtual bool? IsBatchRemoved { get; set; }

        ///<summary>Gets or sets JournalTypeId field.</summary>
        [EnumDataType(typeof(JournalType))]
        public JournalType JournalTypeId { get; set; }

        ///<summary>Gets or sets JournalEntryDetailList field.</summary>
        public List<JournalEntryDetailInputUnit> JournalEntryDetailList { get; set; }
    }
}


