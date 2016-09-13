using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;
using System;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.Banking
{
    /// <summary>
    /// BankRecCleared is the table name in lajit
    /// </summary>
    [Table("CAPS_BankRecCleared")]
    public class BankRecClearedUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        // <summary>
        ///     Maximum length 
        /// </summary>
        public const int MaxLength = 50;

        // <summary>
        ///     Maximum length 
        /// </summary>
        public const int MaxUploadLength = 400;

        #region Class Property Declarations

        /// <summary>Overriding the Id column with BankRecClearedId</summary>
        [Column("BankRecClearedId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the BankAccountId field. </summary>
        public virtual int BankRecControlId { get; set; }       

        /// <summary>Gets or sets the TypeOfAccountingDocumentID field. </summary>
        public virtual TypeOfAccountingDocument? TypeOfAccountingDocumentId { get; set; }

        /// <summary>Gets or sets the AccountingDocumentID field. </summary> 
        public virtual long? AccountingDocumentId { get; set; }

        [ForeignKey("AccountingDocumentId")]
        public virtual AccountingHeaderTransactionsUnit AccountingDocument { get; set; }

        /// <summary>Gets or sets the UploadTrxType field. </summary> 
        [StringLength(MaxLength)]
        public virtual string UploadTrxType { get; set; }

        /// <summary>Gets or sets the BankRecControlID field. </summary> 
        public virtual DateTime? UploadDate { get; set; }

        /// <summary>Gets or sets the UploadNumber field. </summary> 
        [StringLength(MaxLength)]
        public virtual string UploadNumber { get; set; }

        /// <summary>Gets or sets the UploadInfo field. </summary>        
        public virtual string UploadInfo { get; set; }

        /// <summary>Gets or sets the UploadAmount field. </summary> 
        public virtual decimal? UploadAmount { get; set; }

        /// <summary>Gets or sets the IsCleared field. </summary> 
        public virtual bool IsCleared { get; set; }

        /// <summary>Gets or sets the AccountingItemID field. </summary> 
        public virtual long? AccountingItemId { get; set; }
        
        [ForeignKey("AccountingItemId")]
        public virtual AccountingItemUnit AccountingItem { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion


        public BankRecClearedUnit()
        {
            IsCleared = false;
        }
       
    }
}
