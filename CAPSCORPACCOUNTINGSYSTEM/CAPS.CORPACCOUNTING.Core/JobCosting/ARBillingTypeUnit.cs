using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>/// Enum for TypeofBilling/// </summary>
    public enum TypeofBilling
    {
        [Display(Name = "Job Billing")]
        JobBilling = 1,
        [Display(Name = "Non Job Billing")]
        NonJobBilling = 2
    }

    [Table("CAPS_ARBillingTypes")]
    public class ARBillingTypeUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxDescLength = 200;

        #region Class Property Declarations
        /// <summary>Overriding the ID column with ARBillingTypeId</summary>
        [Column("ARBillingTypeId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescLength)]
        public virtual string  Description { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        [Range(0, Int32.MaxValue)]
        public virtual int  JobId { get; set; }
      
        [ForeignKey("JobId")]
        public JobUnit Job { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        [Range(0, Int32.MaxValue)]
        public virtual long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public  AccountUnit Account { get; set; }

        /// <summary>Gets or sets the IsIctBillingType field. </summary>
        public virtual bool IsIctBillingType { get; set; }

        /// <summary>Gets or sets the IsProjectBilling field. </summary>
        public virtual bool IsProjectBilling { get; set; }

        /// <summary>Gets or sets the TypeofBillingId field. </summary>
        [EnumDataType(typeof(TypeofBilling))]
        public virtual TypeofBilling TypeofBillingId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        #endregion

        public ARBillingTypeUnit()
        {
        }

        public ARBillingTypeUnit(string description, int jobid, long accountid, bool isictbillingtype, bool isprojectbilling, long? organizationunitid)
        {
            Description = description;
            JobId = jobid;
            AccountId = accountid;
            IsIctBillingType = isictbillingtype;
            IsProjectBilling = isprojectbilling;
            OrganizationUnitId = organizationunitid;
        }
    }
}