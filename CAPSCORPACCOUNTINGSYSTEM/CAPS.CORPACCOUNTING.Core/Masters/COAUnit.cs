using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// Enum for ChartofAccountsType
    /// </summary>
    public enum ChartofAccountsType
    {
        [Display(Name = "Corporate/Divisions")] Corporate = 1,
        [Display(Name = "Commercial Production (AICP)")] Aicp = 2,
        [Display(Name = "Commercial Editors (AICE)")] Aice = 3,
        [Display(Name = "Episodic Television")] Episodictv = 4,
        [Display(Name = "Feature Film/Television Movie")] Ftv = 5,
        [Display(Name = "Other Projects")] OtherProjects = 6
    }

    /// <summary>
    /// ChartOfAccount  is the table name in lajit
    /// </summary>
    [Table("CAPS_ChartOfAccount")]
    public sealed class CoaUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        ///     Maximum length of the <see cref="Caption" /> property.
        /// </summary>
        public const int MaxDisplayNameLength = 128;

        /// <summary>
        ///     Maximum size of Description.
        /// </summary>
        public const int MaxDesc = 400;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoaUnit" /> class  with no parameter.
        /// </summary>
        public CoaUnit()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoaUnit" /> class.
        /// </summary>
        public CoaUnit(string caption, ChartofAccountsType chartofaccounttype = ChartofAccountsType.Corporate,
            string desc = null, int? displaysequence = null,long? organizationid=null, bool isactive = true, bool isapproved = false,
            bool isprivate = false)
        {
            Caption = caption;
            ChartofAccountsType = chartofaccounttype;
            DisplaySequence = displaysequence;
            IsActive = isactive;
            IsApproved = isapproved;
            IsPrivate = isprivate;
            OrganizationUnitId = organizationid;
            Description = desc;
        }

        #region Class Property Declarations

        /// <summary>Overriding the ID column with COAID</summary>
        [Column("ChartOfAccountId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public int? LajitId { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxDisplayNameLength)]
        [Required]
        public string Caption { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDesc)]
        public string Description { get; set; }

        /// <summary>Gets or sets the COA Type. </summary>
        public ChartofAccountsType ChartofAccountsType { get; set; }

        /// <summary>Gets or sets the Display Sequence. </summary>
        public int? DisplaySequence { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsPrivate field. </summary>
        public bool IsPrivate { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        #endregion
    }
}