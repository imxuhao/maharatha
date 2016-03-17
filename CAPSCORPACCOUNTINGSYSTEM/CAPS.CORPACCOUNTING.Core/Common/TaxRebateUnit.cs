using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    ///  TaxRebate is the table name in Lajit
    /// </summary>
    [Table("CAPS_TaxRebate")]
  public class TaxRebateUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxTaxCodeInfoLength = 100;
        public const int MaxCaptionLength = 20;

        /// <summary> Overriding the ID column with TaxRebateId field. </summary>
        [Column("TaxRebateId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the TaxCodeInfo field. </summary>
        [Required]
        [StringLength(MaxTaxCodeInfoLength)]
        public virtual string TaxCodeInfo { get; set; }

        /// <summary>Gets or sets the TaxFormTypeOfCateGoryId field. </summary>
        public virtual int TaxFormTypeOfCateGoryId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusId field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the EntityId field. </summary>
        public virtual int EntityId { get; set; }
        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        public TaxRebateUnit()
        {
            IsActive = false;
            IsApproved = false;
        }
    }
}
