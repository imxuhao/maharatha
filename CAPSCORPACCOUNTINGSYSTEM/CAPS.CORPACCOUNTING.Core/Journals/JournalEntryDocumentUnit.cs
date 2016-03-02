using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace CAPS.CORPACCOUNTING.Journals
{
    [Table("CAPS_JournalEntryDocument")]
    public class JournalEntryDocumentUnit: AccountingHeaderTransactionsUnit
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
    }
}
