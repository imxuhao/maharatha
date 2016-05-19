using CAPS.CORPACCOUNTING.Accounting;
using System;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Journals
{

    public class UpdateJournalEntryDocumentInputUnit : UpdateAccountingHeaderTransactionInputUnit
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

        //Gets or sets JournalTypeId field.
        [EnumDataType(typeof(JournalType))]
        public JournalType JournalTypeId { get; set; }
    }
}


