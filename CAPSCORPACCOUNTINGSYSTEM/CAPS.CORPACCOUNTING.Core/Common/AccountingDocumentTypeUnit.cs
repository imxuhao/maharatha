using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.Common
{
    // <summary>
    /// AccountingDocumentType is the table name in Lajit
    /// </summary>
    [Table("CAPS_AccountingDocumentType")]
    public class AccountingDocumentTypeUnit : CreationAuditedEntity<short>, IMustHaveTenant
    {      

        public const int MaxCaptionLength = 20;
      
        #region Class Property Declarations

        /// <summary>Overriding the ID column with AccountingDocumentTypeId</summary>
        [Column("AccountingDocumentTypeId")]
        public override short Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual short? LajitId { get; set; }

        /// <summary>Gets or sets the CaptionOverride field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string CaptionOverride { get; set; }

        /// <summary>Gets or sets the TypeOfAccountingDocumentID field. </summary>
        public virtual TypeOfAccountingDocument TypeOfAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        #endregion
    }
}
