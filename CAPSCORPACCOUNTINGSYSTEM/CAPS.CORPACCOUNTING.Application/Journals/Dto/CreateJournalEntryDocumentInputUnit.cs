using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Journals
{
    public class CreateJournalEntryDocumentInputUnit : CreateAccountingHeaderTransactionInputUnit
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

        [Required]
        ///<summary>Get Sets the BatchInfo field.</summary>
        public virtual string BatchInfo { get; set; }

        ///<summary>Get Sets the IsBatchRemoved field.</summary>
        public virtual bool? IsBatchRemoved { get; set; }
    }
}
