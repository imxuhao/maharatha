using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [Table("CAPS_JobPORangeAllocation")]
    public class JobPORangeAllocationUnit : FullAuditedEntity, IMustHaveTenant, IMustHaveOrganizationUnit
    {
        public const int MaxPoRangeStartNumberLength = 50;
        public const int MaxPoRangeEndNumberLength = 50;

        /// <summary>Overriding the ID column with PORangeAllocationId</summary>
        [Column("PORangeAllocationId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public virtual int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual JobUnit Job { get; set; }

        /// <summary>Gets or sets the poRangeStartNumber field. </summary>
        [Required]
        [MaxLength(MaxPoRangeStartNumberLength)]
        public virtual string PoRangeStartNumber { get; set; }

        /// <summary>Gets or sets the poRangeEndNumber field. </summary>
        [Required]
        [MaxLength(MaxPoRangeEndNumberLength)]
        public virtual string PoRangeEndNumber { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public virtual long OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
    }
}
