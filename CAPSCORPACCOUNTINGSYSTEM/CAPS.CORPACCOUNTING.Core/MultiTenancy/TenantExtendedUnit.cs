using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace CAPS.CORPACCOUNTING.MultiTenancy
{
    /// <summary>
    /// TenantExtended Table Added New columns 
    /// </summary>
    [Table("CAPS_TenantExtended")]
    public class TenantExtendedUnit : FullAuditedEntity, IMustHaveTenant
    {
        [Column("TenantExtendedId")]
        public override int Id { get; set; }

        public const int MaxLength = 1000;
        public const int MaxTaxIdLength = 15;

        /// <summary>Gets or sets the TransmitterContactName field. </summary>
        [MaxLength(MaxLength)]
        public virtual string TransmitterContactName { get; set; }

        /// <summary>Gets or sets the TransmitterEmailAddress field. </summary>
        [MaxLength(MaxLength)]
        public virtual string TransmitterEmailAddress { get; set; }

        /// <summary>Gets or sets the TransmitterCode field. </summary>
        [MaxLength(MaxLength)]
        public virtual string TransmitterCode { get; set; }

        /// <summary>Gets or sets the TransmitterControlCode field. </summary>
        [MaxLength(MaxLength)]
        public virtual string TransmitterControlCode { get; set; }

        /// <summary>Gets or sets the FederalTaxID field. </summary>
        [StringLength(MaxTaxIdLength)]
        public string FederalTaxId { get; set; }

        public virtual Guid? CompanyLogoId { get; set; }

        public int TenantId { get; set; }
    }
}
