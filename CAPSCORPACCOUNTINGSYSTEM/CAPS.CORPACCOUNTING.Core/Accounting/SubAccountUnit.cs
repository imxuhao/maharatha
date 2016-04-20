using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using CAPS.CORPACCOUNTING.Banking;

namespace CAPS.CORPACCOUNTING.Accounting
{

    /// <summary>
    ///Enum for TypeofSubAccount 
    /// </summary>
    public enum TypeofSubAccount
    {
        [Display(Name = "Sub Accounts #1")]
        SubAccounts1 = 1,
        [Display(Name = "Sub Account #2")]
        SubAccount2  = 2,
        [Display(Name = "Sub Account #3")]
        SubAccount3 = 3,
        [Display(Name = "Locations")]
        Locations = 4,
        [Display(Name = "Sets")]
        Sets = 5
    }
    /// <summary>
    /// SubAccount is the Table name in Lajit
    /// </summary>
    [Table("CAPS_SubAccount")]
    public class SubAccountUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        // <summary>
        ///     Maximum length 
        /// </summary>
        public const int MaxLength = 100;

        /// <summary>
        /// Maximum caption Length
        /// </summary>
        public const int MaxCaptionLength = 20;

        public const int MaxSerialNoLength = 50;


        #region Declaration of Properties
        /// <summary>Overriding the ID column with UploadDocumentLogId</summary>
        [Column("SubAccountId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual long? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxLength)]
        public string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary> 
        public short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the SubAccountNumber field. </summary>
        [StringLength(MaxLength)]
        public string SubAccountNumber { get; set; }

        /// <summary>Gets or sets the AccountingLayoutItemID field. </summary>
        public int? AccountingLayoutItemId { get; set; }

        /// <summary>Gets or sets the GroupCopyLabel field. </summary>
        [StringLength(MaxCaptionLength)]
        public string GroupCopyLabel { get; set; }

        /// <summary>Gets or sets the IsAccountSpecific field. </summary>
        public bool IsAccountSpecific { get; set; }

        /// <summary>Gets or sets the IsMandatory field. </summary>
        public bool IsMandatory { get; set; }

        /// <summary>Gets or sets the IsBudgetInclusive field. </summary>
        public bool IsBudgetInclusive { get; set; }

        /// <summary>Gets or sets the IsCorporateSubAccount field. </summary>// IsBudgetInclusive
        public bool IsCorporateSubAccount { get; set; }

        /// <summary>Gets or sets the IsProjectSubAccount field. </summary>
        public bool IsProjectSubAccount { get; set; }

        /// <summary>Gets or sets the EntityID field. </summary>
        public int EntityId { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary>
        public TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }
      
        /// <summary>Gets or sets the IsEnterable field. </summary>
        public bool? IsEnterable { get; set; }

        /// <summary>Gets or sets the SearchOrder field. </summary> 
        public long? SearchOrder { get; set; }

        /// <summary>Gets or sets the SearchNo field. </summary>
        [StringLength(MaxSerialNoLength)]
        public string SearchNo { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the TypeofSubAccountId field. </summary>
        [EnumDataType(typeof(TypeofSubAccount))]
        public virtual TypeofSubAccount TypeofSubAccountId { get; set; }
        #endregion

        public SubAccountUnit()
        {
            IsAccountSpecific = false;
            IsMandatory = false;
            IsBudgetInclusive = false;
            IsCorporateSubAccount = false;
            IsProjectSubAccount = false;
            IsApproved = false;
            IsActive = false;
            IsEnterable = false;
        }
    }
}
