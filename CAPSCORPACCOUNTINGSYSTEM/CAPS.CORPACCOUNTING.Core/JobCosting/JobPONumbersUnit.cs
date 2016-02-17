using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
namespace CAPS.CORPACCOUNTING.Masters
{
    [Table("CAPS_JobPONumbers")]
    public class JobPONumbersUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxPoNumberLength = 50;
        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobPONumberId</summary>
        [Column("JobPONumberId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the StartingPONumber field. </summary>
        [Required]
        [StringLength(MaxPoNumberLength)]
        public virtual string StartingPONumber { get; set; }

        /// <summary>Gets or sets the EndingPONumber field. </summary>
        [Required]
        [StringLength(MaxPoNumberLength)]
        public virtual string EndingPONumber { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

        public JobPONumbersUnit()
        {
        }
        public JobPONumbersUnit(string startingponumber, string endingponumber, long? organizationunitid)
        {
            StartingPONumber = startingponumber;
            EndingPONumber = endingponumber;
            OrganizationUnitId = organizationunitid;
        }

    }
}