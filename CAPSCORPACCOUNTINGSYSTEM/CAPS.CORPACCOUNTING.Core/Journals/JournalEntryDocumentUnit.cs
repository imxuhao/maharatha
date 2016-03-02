using CAPS.CORPACCOUNTING.Banking;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace CAPS.CORPACCOUNTING.Journals
{
    [Table("CAPS_JournalEntryDocument")]
    public class JournalEntryDocumentUnit
    {
        public virtual int? BatchId { get; set; }

        [ForeignKey("BatchId")]
        public virtual BatchUnit Batch { get; set; }
        public virtual bool IsReversingEntry { get; set; }

        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? DateOfReversal { get; set; }

        public virtual bool IsRecurringEntry { get; set; }

        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? DateToRecur { get; set; }

        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? FinalDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public virtual SqlDateTime? LastPostDate { get; set; }     
        public virtual string BatchInfo { get; set; }
        public virtual bool? IsBatchRemoved { get; set; }
    }
}
