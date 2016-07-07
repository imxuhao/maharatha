using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.EFAuditLog;
using CAPS.CORPACCOUNTING.JobCosting;

namespace CAPS.CORPACCOUNTING.Masters
{  
    public enum StandardGroupTotal
    {
        [Display(Name="AICETOTALING")]
        AICETOTALING=1,
        [Display(Name = "STANDARD FINANCIALS")]
        STANDARDFINANCIALS=2,
        [Display(Name = "AICP TOTALING")]
        AICPTOTALING=3,
        [Display(Name = "VENDOR TOTALS")]
        VENDORTOTALS=4,
        [Display(Name = "PC TOTALS")]
        PCTOTALS=5,
        [Display(Name = "INCOME STATEMENT")]
        INCOMESTATEMENT=6,
        [Display(Name = "ACCOUNT GROUP")]
        ACCOUNTGROUP=7,
        [Display(Name = "BALANCE SHEET")]
        BALANCESHEET=8
    }

    /// <summary>
    /// ChartOfAccount  is the table name in lajit
    /// </summary>
    [Table("CAPS_ChartOfAccount")]
    public class CoaUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit,INeedModLog
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
        public CoaUnit(string caption, long? organizationid,string desc = null,
            int? displaysequence = null, bool isactive = true, 
            bool isapproved = false,bool isprivate = false)
        {
            Caption = caption;           
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
        public virtual  string Caption { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDesc)]
        public virtual  string Description { get; set; }      

        /// <summary>Gets or sets the Display Sequence. </summary>
        public virtual  int? DisplaySequence { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual  bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsPrivate field. </summary>
        public virtual  bool IsPrivate { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual  int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual  long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual  bool IsActive { get; set; }

        /// <summary>Gets or sets the IsCorporate field. </summary>
        public virtual  bool IsCorporate { get; set; }

        /// <summary>Gets or sets the IsNumeric field. </summary>
        public virtual  bool IsNumeric { get; set; }

        /// <summary>Gets or sets the LinkChartOfAccountID field. </summary>
        public virtual  int? LinkChartOfAccountID { get; set; }

        /// <summary>Gets or sets the RollupAccountId field. </summary>
        public virtual long? RollupAccountId { get; set; }

       

        /// <summary>Gets or sets the RollupDivisionId field. </summary>
        public virtual int? RollupDivisionId { get; set; }

      


        /// <summary>Gets or sets the StandardGroupTotalId field. </summary>      
        public virtual  StandardGroupTotal? StandardGroupTotalId { get; set; }

        #endregion
    }
}