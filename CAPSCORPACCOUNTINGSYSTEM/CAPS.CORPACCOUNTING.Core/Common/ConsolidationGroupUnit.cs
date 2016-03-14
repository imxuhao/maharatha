using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    /// ConsolidationGroup is the table name in Lajit
    /// </summary>
    [Table("CAPS_ConsolidationGroup")]
    public class ConsolidationGroupUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the ID column with ConsolidationGroupId</summary>
        [Column("ConsolidationGroupId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the TypeOfCategoryID field. </summary>
        public short TypeOfCategoryId { get; set; }

        [ForeignKey("TypeOfCategoryId")]
        public TypeOfCategoryUnit TypeOfCategory { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the IsYTDIncluded field. </summary>
        public bool? IsYtdIncluded { get; set; }

        /// <summary>Gets or sets the IsQuarterly field. </summary>
        public bool? IsQuarterly { get; set; }

        /// <summary>Gets or sets the IsEliminationIncluded field. </summary>
        public bool? IsEliminationIncluded { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }
        #endregion
        public ConsolidationGroupUnit()
        {
            IsActive = true;

        }
    }
}
