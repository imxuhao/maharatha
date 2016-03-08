using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Accounting
{

    /// <summary>
    /// Enum for TypeOfLayout
    /// </summary>
    public enum TypeOfLayout
    {
        [Display(Name = "Accounting Entry Layout")]
        AccountingEntryLayout = 1,
        [Display(Name = "Corporate Ledger Budget Layout")]
        CorporateLedgerBudgetLayout = 2,
        [Display(Name = "Sub Ledger Budget Layout")]
        SubLedgerBudgetLayout = 3

    }

    /// <summary>
    /// AccountingLayout is the Table name in Lajit
    /// </summary>
    [Table("CAPS_AccountingLayout")]
    public class AccountingLayoutUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        // <summary>
        ///     Maximum length 
        /// </summary>
        public const int MaxDescLength = 500;

        public const int MaxCaptionLength = 20;

        #region Declaration of Properties
        /// <summary>Overriding the ID column with AccountingLayoutId</summary>
        [Column("AccountingLayoutId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; } // Description

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; } 

        /// <summary>Gets or sets the TypeOfLayoutID field. </summary>
        public virtual TypeOfLayout? TypeOfLayoutId { get; set; } 

        /// <summary>Gets or sets the CopyTypeOfHeadingGroupID field. </summary>
        public virtual short? CopyTypeOfHeadingGroupId { get; set; } 

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; } 

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }  

        /// <summary>Gets or sets the EntityID field. </summary>
        public virtual int EntityId { get; set; }  
       
        /// <summary>Gets or sets the CopyTrxID field. </summary>
        public virtual int? CopyTrxId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        #endregion

       
    }
}
