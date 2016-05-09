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

namespace CAPS.CORPACCOUNTING.Masters
{

    [Table("CAPS_TaxCredit")]
    public class TaxCreditUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxNumberLength = 5;
        public const int MaxDescriptionLength = 50;

        /// <summary>Gets or sets the AccountId field. </summary>
        [Column("TaxCreditId")]
        public override long Id { get; set; }

        /// <summary>Gets or sets the Number field. </summary>
        [Required]
        [MaxLength(MaxNumberLength)]
        public virtual string Number { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [MaxLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public bool IsDefault { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }


    }
}
