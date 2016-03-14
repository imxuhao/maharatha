using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    ///  SICCode is the table name in Lajit
    /// </summary>
    [Table("CAPS_SICCode")]
    public class SICCodeUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        /// <summary> Overriding the ID column with SicCodeId field. </summary>
        [Column("SicCodeId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the TypeOfSicCodeId field. </summary>
        public virtual int? TypeOfSicCodeId { get; set; }
        [ForeignKey("TypeOfSicCodeId")]
        public virtual TypeOfSicCodeUnit TypeOfSicCodeUnit { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public virtual int? VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual VendorUnit VendorUnit { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        public virtual long? AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual AccountUnit AccountUnit { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public virtual int? JobId { get; set; }
        [ForeignKey("JobId")]
        public virtual JobUnit JobUnit { get; set; }

        /// <summary>Gets or sets the EntityId field. </summary>
        public virtual int? EntityId { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool? IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInActiveStatusId field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInActiveStatusId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }


        public SICCodeUnit()
        {
            IsApproved = false;
            IsActive = true;
        }


    }
}
