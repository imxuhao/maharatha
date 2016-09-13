using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Accounting
{
    /// <summary>
    /// Enum for TypeOfAccountingControl
    /// </summary>
    public enum TypeOfAccountingControl
    {

        [Display(Name = "Ledger Reference")]
        LedgerReference = 1,
        [Display(Name = "Default Rollup")]
        DefaultRollup = 2,
        [Display(Name = "Last A/R Invoice")]
        LastARInvoice = 3,
        [Display(Name = "A/R Invoice# When Adding")]
        ARInvoiceNumberWhenAdding = 4,
        [Display(Name = "Last PO# used")]
        LastPONumberused = 5,
        [Display(Name = "PO Override AutoOff")]
        POOverrideAutoOff = 6,
        [Display(Name = "Last JE")]
        LastJE = 7,
        [Display(Name = "A/R Aging Column 1")]
        ARAgingColumn1 = 8,
        [Display(Name = "A/R Aging Column 2")]
        ARAgingColumn2 = 9,
        [Display(Name = "A/R Aging Column 3")]
        ARAgingColumn3 = 10

    }
    /// <summary>
    /// AccountingControl is the Table name in Lajit
    /// </summary>
    [Table("CAPS_AccountingControl")]
    public class AccountingControlUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        // <summary>
        ///     Maximum length 
        /// </summary>
        public const int MaxDescLength = 500;

        public const int MaxCaptionLength = 20;

        #region Declaration of Properties
        /// <summary>Overriding the ID column with AccountingControlId</summary>
        [Column("AccountingControlId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the TypeOfAccountingControlID field. </summary>
        public virtual TypeOfAccountingControl TypeOfAccountingControlId { get; set; }

        /// <summary>Gets or sets the ControlValue field. </summary>
        public virtual string ControlValue { get; set; }

        /// <summary>Gets or sets the ControlCount field. </summary>
        public virtual int? ControlCount { get; set; }

        /// <summary>Gets or sets the EntityID field. </summary>
        public virtual int? EntityId { get; set; }

        [ForeignKey("EntityId")]
        public virtual EntityUnit Entity { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        #endregion


        public AccountingControlUnit()
        {
            IsActive = false;
            IsApproved = false;
        }
    }


}
