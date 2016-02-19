using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// Enum for RollupType
    /// </summary>
    public enum RollupType
    {
        [Display(Name = "Projects")]
        Projects = 1,
        [Display(Name = "Corporates")]
        Corporates = 2
    }
    [Table("CAPS_RollupCenter")]
    public class RollupCenterUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxCaptionLength = 500;

        /// <summary>Overriding the ID column with RollupCenterId</summary>
        #region Declaration of Properties
        [Column("RollupCenterId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        [Required]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public virtual long? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit Account { get; set; }

        /// <summary>Gets or sets the Job field. </summary>
        public virtual int? JobId { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }
        /// <summary>Gets or sets the RollupTypeID field. </summary>
        public virtual RollupType RollupTypeId { get; set; }

        #endregion

        public RollupCenterUnit()
        {
        }
        public RollupCenterUnit(string caption, long? accountid, int? jobid, bool isactive, bool isapproved, long? organizationunitid, RollupType rolluptypeid)
        {
            Caption = caption;
            AccountId = accountid;
            JobId = jobid;
            IsActive = isactive;
            IsApproved = isapproved;
            OrganizationUnitId = organizationunitid;
            RollupTypeId = rolluptypeid;
        }
    }
}