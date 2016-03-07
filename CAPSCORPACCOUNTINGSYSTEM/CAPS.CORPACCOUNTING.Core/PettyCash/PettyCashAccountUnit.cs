using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.JobCosting;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.PettyCash
{
    /// <summary>
    /// PettyCashAccount is the Table name in Lajit
    /// </summary>
    [Table("CAPS_PettyCashAccount")]
    public class PettyCashAccountUnit : FullAuditedEntity<long>, IMustHaveTenant, IMustHaveOrganizationUnit
    {
        /// <summary>
        ///     Maximum length 
        /// </summary>
        public const int MaxLength = 200;

        #region Declaration of Properties
        /// <summary>Overriding the ID column with PettyCashAccountId</summary>
        [Column("PettyCashAccountId")]
        public override long Id { get; set; }

        /// <summary>Gets or sets the VendorID field. </summary>
        public virtual int VendorId { get; set; } 

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public virtual long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual AccountUnit Account { get; set; }

        /// <summary>Gets or sets the JobID field. </summary>
        public virtual int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual JobUnit Job { get; set; }

        /// <summary>Gets or sets the PayToName field. </summary>
        [StringLength(MaxLength)]
        public virtual string PayToName { get; set; }

        /// <summary>Gets or sets the FloatAmount field. </summary>
        public virtual decimal? FloatAmount { get; set; }
       
        /// <summary>Gets or sets the IsCustodian field. </summary>
        public virtual bool IsCustodian { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; } // IsActive

        /// <summary>Gets or sets the TypeOfInactiveStatusid field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusid { get; set; } 

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; } // IsApproved
      
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long OrganizationUnitId {  get;  set;}

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
       
        #endregion
        public PettyCashAccountUnit()
        {
            IsCustodian = false;
            IsActive = true;
            IsApproved = false;
        }
    }
}
