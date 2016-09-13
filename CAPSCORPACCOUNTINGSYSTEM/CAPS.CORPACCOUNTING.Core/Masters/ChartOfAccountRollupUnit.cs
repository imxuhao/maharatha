using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters
{
    /// <summary>
    /// ChartOfAccountRollup is the table name in lajit
    /// </summary>
    [Table("CAPS_ChartOfAccountRollup")]
    public class ChartOfAccountRollupUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        /// Max Length of Description
        /// </summary>
        public const int MaxLength = 100;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with ChartOfAccountRollupId</summary>
        [Column("ChartOfAccountRollupId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the ChartOfAccountID field. </summary>
        public virtual int ChartOfAccountId { get; set; }

        [ForeignKey("ChartOfAccountId")]
        public CoaUnit ChartofAccount { get; set; }

        /// <summary>Gets or sets the LinkChartOfAccountID field. </summary>
        public virtual int? LinkChartOfAccountId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; } 

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool? IsActive { get; set; } 

        /// <summary>Gets or sets the TypeOfInActiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInActiveStatusId { get; set; } 

        /// <summary>Gets or sets the TypeOfValidationCategoryID field. </summary>
        public virtual short? TypeOfValidationCategoryId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

    }
}
