using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    /// ConsolidationDetail is the Table name in Lajit
    /// </summary>
    [Table("CAPS_ConsolidationDetail")]
    public class ConsolidationDetailUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Declaration of Properties
        /// <summary>Overriding the ID column with ConsolidationDetailId</summary>
        [Column("ConsolidationDetailId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the ConsolidationGroupID field. </summary>
        public virtual int ConsolidationGroupId { get; set; }

        [ForeignKey("ConsolidationGroupId")]
        public virtual ConsolidationGroupUnit ConsolidationGroup { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual int? DisplaySequence { get; set; }

        /// <summary>Gets or sets the LinkCompanyID field. </summary>
        public virtual int? LinkCompanyId { get; set; }

        /// <summary>Gets or sets the LinkCostCenterID field. </summary>
        public virtual int? LinkCostCenterId { get; set; }

        /// <summary>Gets or sets the LinkJobID field. </summary>
        public virtual int? LinkJobId { get; set; }

        /// <summary>Gets or sets the LinkSubAccountID1 field. </summary>
        public virtual int? LinkSubAccountId1 { get; set; }

        /// <summary>Gets or sets the LinkSubAccountID2 field. </summary>
        public virtual int? LinkSubAccountId2 { get; set; }

        /// <summary>Gets or sets the LinkSubAccountID3 field. </summary>
        public virtual int? LinkSubAccountId3 { get; set; }

        /// <summary>Gets or sets the LinkSubAccountID4 field. </summary>
        public virtual int? LinkSubAccountId4 { get; set; }

        /// <summary>Gets or sets the LinkSubAccountID5 field. </summary>
        public virtual int? LinkSubAccountId5 { get; set; }

        /// <summary>Gets or sets the LinkSubAccountID6 field. </summary>
        public virtual int? LinkSubAccountId6 { get; set; }

        /// <summary>Gets or sets the LinkSubAccountID7 field. </summary>
        public virtual int? LinkSubAccountId7 { get; set; }

        /// <summary>Gets or sets the LinkSubAccountID8 field. </summary>
        public virtual int? LinkSubAccountId8 { get; set; }

        /// <summary>Gets or sets the LinkSubAccountID9 field. </summary>
        public virtual int? LinkSubAccountId9 { get; set; }

        /// <summary>Gets or sets the LinkSubAccountID10 field. </summary>
        public virtual int? LinkSubAccountId10 { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the TypeOfAccountId field. </summary>
        public virtual int? TypeOfAccountId { get; set; }

        [ForeignKey("TypeOfAccountId")]
        public virtual TypeOfAccountUnit TypeOfAccount { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        #endregion

        public ConsolidationDetailUnit()
        {
            IsActive = true;
        }
    }
}
