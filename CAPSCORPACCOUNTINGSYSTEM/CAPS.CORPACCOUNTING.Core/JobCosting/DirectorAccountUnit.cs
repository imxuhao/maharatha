using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [Table("CAPS_DirectorAccount")]
    public class DirectorAccountUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Declaration of Properties
        /// <summary>Overriding the ID column with DirectorAccountId</summary>
        [Column("DirectorAccountId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        [Range(0, Int32.MaxValue)]
        public virtual int? JobId { get; set; }

        [ForeignKey("JobId")]
        public JobUnit Job { get; set; }

        /// <summary>Gets or sets the DirectorId field. </summary>
        [Range(0, Int32.MaxValue)]
        public virtual int? DirectorId { get; set; }

        [ForeignKey("DirectorId")]
        public DirectorUnit Director { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        [Range(0, Int32.MaxValue)]
        public virtual long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public AccountUnit Account { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }
        #endregion

        public DirectorAccountUnit()
        {
        }

        public DirectorAccountUnit(int? directorid, long accountid, int? jobid)
        {
            DirectorId = directorid;
            AccountId = accountid;
            JobId = jobid;


        }

    }
}
