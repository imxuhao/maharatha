using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.AccountReceivable
{
    /// <summary>
    /// TypeOfARInvoiceBuild is the table name in lajit
    /// </summary>
    [Table("CAPS_TypeOfARInvoiceBuild")]
    public class TypeOfArInvoiceBuildUnit : CreationAuditedEntity<short>
    {
        public const int MaxDescLength = 100;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with TypeOfARInvoiceBuildID</summary>
        [Column("TypeOfArInvoiceBuildId")]
        public override short Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>      
        [Required]
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }
        #endregion
    }
}
