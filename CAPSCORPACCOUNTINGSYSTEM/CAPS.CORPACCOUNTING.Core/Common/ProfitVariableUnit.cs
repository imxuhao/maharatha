using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    ///  ProfitVariable is the table name in Lajit
    /// </summary>
    [Table("CAPS_ProfitVariable")]
    public class ProfitVariableUnit: FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        /// <summary> Overriding the ID column with ProfitVariableId field. </summary>
        [Column("ProfitVariableId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the ProfitId field. </summary>
        public virtual int ProfitId { get; set; }

        /// <summary>Gets or sets the VariableTypeOfAmountId field. </summary>
        public virtual short VariableTypeOfAmountId { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public virtual int? JobId { get; set; }
        [ForeignKey("JobId")]
        public virtual JobUnit JobUnit { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public virtual long? AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual AccountUnit AccountUnit { get; set; }

        /// <summary>Gets or sets the SubAccountId field. </summary>
        public virtual long? SubAccountId { get; set; }
        [ForeignKey("SubAccountId")]
        public virtual SubAccountUnit SubAccountUnit { get; set; }

        /// <summary>Gets or sets the BillingTypeId field. </summary>
        public virtual int? BillingTypeId { get; set; }

        /// <summary>Gets or sets the SourceTypeOfAmountId field. </summary>
        public virtual short SourceTypeOfAmountId { get; set; }

        /// <summary>Gets or sets the AdjustmentTypeOfAmountId field. </summary>
        public virtual short AdjustmentTypeOfAmountId { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeofInactiveStatusId field. </summary>
        public virtual TypeOfInactiveStatus? TypeofInactiveStatusId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        public  ProfitVariableUnit()
        {
            IsApproved = false;
            IsActive = true;
        }

    }
}
