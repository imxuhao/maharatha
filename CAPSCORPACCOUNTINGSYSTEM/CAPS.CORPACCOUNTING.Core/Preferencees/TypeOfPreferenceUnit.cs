using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Preferencees
{
    /// <summary>
    ///Enum for TypeOfPreferenceCategory
    /// </summary>
    public enum TypeOfPreferenceCategory
    {
        [Display(Name = "Approvals")]
        Approvals = 1,
        [Display(Name = "Broadcasts")]
        Broadcasts = 2,
        [Display(Name = "Chain of Command")]
        ChainofCommand = 3,
        [Display(Name = "DashBoard Preferences")]
        DashBoardPreferences = 4,
        [Display(Name = "Company Business Rules")]
        CompanyBusinessRules = 5,
        [Display(Name = "Entity Business Rules")]
        EntityBusinessRules = 6,
        [Display(Name = "Tenant Business Rules")]
        TenantBusinessRules = 7,
        [Display(Name = "Role Preferences")]
        RolePreferences = 8,
        [Display(Name = "User Preference")]
        UserPreference = 9,
        [Display(Name = "Accounts Payable Business Rules")]
        AccountsPayableBusinessRules = 10,
        [Display(Name = "Accounts Receivable Business Rules")]
        AccountsReceivableBusinessRules = 11,
        [Display(Name = "Payroll Business Rules")]
        PayrollBusinessRules = 12,
        [Display(Name = "General Ledger Business Rules")]
        GeneralLedgerBusinessRules = 13,
        [Display(Name = "System Preferences")]
        SystemPreferences = 14,
        [Display(Name = "Grid Views")]
        GridViews = 15,
        [Display(Name = "Petty Cash Rules")]
        PettyCashRules = 16,
        [Display(Name = "Retired")]
        Retired = 17
    }

    /// <summary>
    /// TypeOfPreference is the table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfPreference")]
    public class TypeOfPreferenceUnit : FullAuditedEntity<short>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        /// Maximum Length of Description
        /// </summary>
        public const int MaxDescLength = 100;
        
        /// <summary> Maximum Length of Caption </summary>
        public const int MaxCaptionLength = 20;

        #region Class Property Declarations

        /// <summary>Overriding the Id column with TypeOfPreferenceId</summary>
        [Column("TypeOfPreferenceId")]
        public override short Id { get; set; }

        /// <summary>Gets or sets the TypeOfPreferenceCategoryID field. </summary>
        public virtual TypeOfPreferenceCategory TypeOfPreferenceCategoryId { get; set; } 

        /// <summary>Gets or sets the PreferenceChoiceGroupID field. </summary>
        public virtual int? PreferenceChoiceGroupId { get; set; } 

        [ForeignKey("PreferenceChoiceGroupId")]
        public PreferenceChoiceGroupUnit PreferenceChoiceGroup { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; } 

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

    }
}
