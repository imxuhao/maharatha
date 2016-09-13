using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;

namespace CAPS.CORPACCOUNTING.AccountReceivable
{  

    /// <summary>
    /// ARInvoiceBuild is the table name in lajit
    /// </summary>
    [Table("CAPS_ARInvoiceBuild")]
    public class ArInvoiceBuildUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {       
        #region Class Property Declarations

        /// <summary>Overriding the ID column with ARInvoiceBuildID</summary>
        [Column("ARInvoiceBuildID")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual long? LajitId { get; set; }

        /// <summary>Gets or sets the TypeOfARInvoiceBuildID field. </summary>
        public virtual short? TypeOfArInvoiceBuildId { get; set; } 

        /// <summary>Gets or sets the Description field. </summary>
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public virtual short? DisplaySequence { get; set; } 

        /// <summary>Gets or sets the CalcPercent field. </summary>
        public virtual decimal? CalcPercent { get; set; } 

        /// <summary>Gets or sets the IsLastCalc field. </summary>
        public virtual bool IsLastCalc { get; set; } 

        /// <summary>Gets or sets the ARBillingTypeID field. </summary>
        public virtual int? ArBillingTypeId { get; set; } 

        /// <summary>Gets or sets the ARPaymentTermID field. </summary>
        public virtual int? ArPaymentTermId { get; set; } 

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; } // 

        /// <summary>Gets or sets the EntityID field. </summary>
        public virtual int EntityId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

        public ArInvoiceBuildUnit()
        {
            IsLastCalc = false;
            IsActive = true;
            IsApproved = false;
        }
    }
}
