using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// JobAccount is the table name in lajit
    /// </summary>
    [Table("CAPS_JobAccount")]
    public class JobAccountUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxDescriptionLength = 200;
        public const int MaxRollUpAcccountDescriptionLength = 400;
        public const int MaxRollUpJobDescriptionLength = 200;

        #region Declaration of Properties
        /// <summary>Overriding the Id column with JobAccountId</summary>
        [Column("JobAccountId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        [Range(0, Int32.MaxValue)]
        public virtual int JobId { get; set; }

        [ForeignKey("JobId")]
        public JobUnit Job { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        [Range(0, Int32.MaxValue)]
        public virtual long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public AccountUnit Account { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the RollupJobId field. </summary>
        public virtual int? RollupJobId { get; set; }

        [ForeignKey("RollupJobId")]
        public virtual JobUnit RollupJob { get; set; }

        /// <summary>Gets or sets the RollupAccountId field. </summary>
        public virtual long? RollupAccountId { get; set; }

        [ForeignKey("RollupAccountId")]
        public virtual AccountUnit RollupAccount { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the RollupAccountDescription field. </summary>
        [MaxLength(MaxRollUpAcccountDescriptionLength)]
        public virtual string RollupAccountDescription { get; set; }

        /// <summary>Gets or sets the RollupJobDescription field. </summary>
        [MaxLength(MaxRollUpJobDescriptionLength)]
        public virtual string RollupJobDescription { get; set; }


        #endregion



        public JobAccountUnit()
        {
            
        }
        public JobAccountUnit(int jobid, long accountid, string description, int? rollupjobid, long? rollupaccountId, long? organizationunitid, string rollupaccountdescription, string rollupjobdescription)
        {
            JobId = jobid;
            AccountId = accountid;
            Description = description;
            RollupJobId = rollupjobid;
            RollupAccountId = rollupaccountId;
            OrganizationUnitId = organizationunitid;
            RollupAccountDescription = rollupaccountdescription;
            RollupJobDescription = rollupjobdescription;
        }

    }
}
