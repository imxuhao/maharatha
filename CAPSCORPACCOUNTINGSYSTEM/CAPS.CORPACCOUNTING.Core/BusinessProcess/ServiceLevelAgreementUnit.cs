using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.BusinessProcess
{
    public enum TypeOfServiceLevelAgreement
    {
        [Display(Name = "Internal Business Level Agreement")]
        InternalBusinessLevelAgreement = 1,
        [Display(Name = "FEDEX")]
        FEDEX = 2,
        [Display(Name = "American Express")]
        AmericanExpress = 3,
        [Display(Name = "Sony Pictures")]
        SonyPictures = 4,
    }

    /// <summary>
    ///  ServiceLevelAgreement is the table name in Lajit
    /// </summary>
    [Table("CAPS_ServiceLevelAgreement")]
    public class ServiceLevelAgreementUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        private const int MaxCaptionLength = 20;

        /// <summary> Overriding the ID column with ServiceLevelAgreementId field. </summary>
        [Column("ServiceLevelAgreementId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [MaxLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }

        /// <summary>Gets or sets the TypeOfServiceLevelAgreementId field. </summary>
        public TypeOfServiceLevelAgreement TypeOfServiceLevelAgreementId { get; set; }

        /// <summary>Gets or sets the TransactionVolume field. </summary>
        public short? TransactionVolume { get; set; }

        /// <summary>Gets or sets the DayHourMinSecResponse field. </summary>
        public short? DayHourMinSecResponse { get; set; }

        /// <summary>Gets or sets the StatusMessageId field. </summary>
        public int? StatusMessageId { get; set; }

        /// <summary>Gets or sets the WarningMessageId field. </summary>
        public int? WarningMessageId { get; set; }

        /// <summary>Gets or sets the ErrorMessageId field. </summary>
        public int? ErrorMessageId { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
