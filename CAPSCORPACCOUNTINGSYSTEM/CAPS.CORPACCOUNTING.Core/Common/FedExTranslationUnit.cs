using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    ///  FedExTranslation is the table name in Lajit
    /// </summary>
    [Table("CAPS_FedExTranslation")]
   public class FedExTranslationUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxRangeStartLength= 100;
        public const int MaxRangeEndLength = 100;
        public const int MaxLineNumberLength = 100;
        public const int MaxInvoiceTypeLength = 50;

        /// <summary> Overriding the ID column with FedExTranslationId field. </summary>
        [Column("FedExTranslationId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the VendorTypeId field. </summary>
        public virtual TypeofVendor VendorTypeId { get; set; }

        /// <summary>Gets or sets the JobNumberRangeStart field. </summary>
        [Required]
        [StringLength(MaxRangeStartLength)]
        public virtual string JobNumberRangeStart { get; set; }

        /// <summary>Gets or sets the JobNumberRangeEnd field. </summary>
        [StringLength(MaxRangeEndLength)]
        public virtual string JobNumberRangeEnd { get; set; }

        /// <summary>Gets or sets the Linenumber field. </summary>
        [Required]
        [StringLength(MaxLineNumberLength)]
        public virtual string Linenumber { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool? IsActive { get; set; }

        /// <summary>Gets or sets the InvoiceType field. </summary>
        [StringLength(MaxInvoiceTypeLength)]
        public virtual string InvoiceType { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
