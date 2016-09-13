using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [Table("CAPS_JobPCConfig")]
    public class JobPCConfigUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobPCConfigId</summary>
        [Column("JobPCConfigId")]
        public override int Id { get; set; }


        /// <summary>Gets or sets the JobId field. </summary>
        [Range(0,Int32.MaxValue)]
        public virtual int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual JobUnit Job { get; set; }

        /// <summary>Gets or sets the AccountId field. </summary>
        [Range(0, Int32.MaxValue)]
        public virtual long AccountId { get; set; }

       [ForeignKey("AccountId")]
        public virtual AccountUnit Account { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        [Range(0, Int32.MaxValue)]
        public virtual int VendorId { get; set; }

        [ForeignKey("VendorId")]
        public virtual VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        #endregion

        public JobPCConfigUnit()
        {
        }
        public JobPCConfigUnit(int jobid, int accountid, int vendorid, long? organizationunitid)
        {
            JobId = jobid;
            AccountId = accountid;
            VendorId = vendorid;
            OrganizationUnitId = organizationunitid;
        }
    }
}