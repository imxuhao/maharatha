using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Accounting
{
    /// <summary>
    /// TypeOfAccountClassification is the table name in lajit
    /// </summary>
    [Table("CAPS_TypeOfAccountClassification")]
    public class TypeOfAccountClassificationUnit : FullAuditedEntity<short>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxDescLength = 100;
        public const int MaxCaptionLength = 20;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with TypeOfAccountClassificationId</summary>
        [Column("TypeOfAccountClassificationId")]
        public override short Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; } 

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the IsAccountSignPositive field. </summary>
        public virtual bool IsAccountSignPositive { get; set; } 

        /// <summary>Gets or sets the IsBalanceSheetAccount field. </summary>
        public virtual bool IsBalanceSheetAccount { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

    }
}
