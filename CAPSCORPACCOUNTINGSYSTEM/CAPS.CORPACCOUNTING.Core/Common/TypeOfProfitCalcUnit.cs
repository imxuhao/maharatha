using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    /// TypeOfProfitCalc is the table name in Lajit
    /// </summary>
    [Table("CAPS_TypeOfProfitCalc")]
   public class TypeOfProfitCalcUnit : CreationAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxDescriptionLength = 400;
        public const int MaxCaptionLength = 20;

        /// <summary> Overriding the ID column with TypeOfProfitCalcId field. </summary>
        [Column("TypeOfProfitCalcId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
