using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Masters
{
    public enum ChartofAccountsType
    {

        [Display(Name = "Corporate/Divisions")]
        CORPORATE = 1,
        [Display(Name = "Commercial Production (AICP)")]
        AICP = 2,
        [Display(Name = "Commercial Editors (AICE)")]
        AICE = 3,
        [Display(Name = "Episodic Television")]
        EPISODICTV = 4,
        [Display(Name = "Feature Film/Television Movie")]
        FTV = 5,
        [Display(Name = "Other Projects")]
        OtherProjects = 6
    }

    [Table("ChartOfAccounts")]
    public class CoaUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        /// Maximum length of the <see cref="Caption"/> property.
        /// </summary>
        public const int MaxDisplayNameLength = 128;
        /// <summary>
        /// Maximum size of Description.
        /// </summary>
        public const int MaxDesc = 400;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with COAID</summary>
        [Column("COAId")]
        public override int Id { get; set; }

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
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CoaUnit"/> class  with no parameter.
        /// </summary>
        public CoaUnit()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoaUnit"/> class.
        /// </summary>
        public CoaUnit(string caption, ChartofAccountsType chartofaccounttype = ChartofAccountsType.CORPORATE, string desc = null, int? displaysequence = null, bool isactive = true, bool isapproved = false,
           bool isprivate = false)
        {
            Caption = caption;
            ChartofAccountsType = chartofaccounttype;
            DisplaySequence = displaysequence;
            IsActive = isactive;
            IsApproved = isapproved;
            IsPrivate = isprivate;
            IsActive = isactive;
           

        }


    }
}
