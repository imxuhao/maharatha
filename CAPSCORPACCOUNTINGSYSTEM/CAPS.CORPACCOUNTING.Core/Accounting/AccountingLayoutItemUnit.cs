using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using System;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.Accounting
{
    /// <summary>
    ///  AccountingLayoutItem is the Table name in Lajit
    /// </summary>
    [Table("CAPS_ AccountingLayoutItem")]
    public class AccountingLayoutItemUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        // <summary>
        ///     Maximum length 
        /// </summary>
        public const int MaxLength = 50;

        #region Declaration of Properties
        /// <summary>Overriding the ID column with AccountingLayoutItemId</summary>
        [Column("AccountingLayoutItemId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the AccountingLayoutID field. </summary>
        public int AccountingLayoutId { get; set; } 

        /// <summary>Gets or sets the TypeOfAccountingLayoutID field. </summary>
        public int TypeOfAccountingLayoutId { get; set; } 

        [ForeignKey("TypeOfAccountingLayoutId")]
        public TypeOfAccountingLayoutUnit TypeOfAccountingLayout { get; set; }

        /// <summary>Gets or sets the TypeOfHeadingID field. </summary>
        public int TypeOfHeadingId { get; set; }

        [ForeignKey("TypeOfHeadingId")]
        public TypeOfHeadingUnit TypeOfHeading { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public short DisplaySequence { get; set; } 

        /// <summary>Gets or sets the IsDisplayedOnFirstPage field. </summary>
        public bool IsDisplayedOnFirstPage { get; set; } 

        /// <summary>Gets or sets the IsHidden field. </summary>
        public bool IsHidden { get; set; } 

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; } 

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        #endregion


    }
}
