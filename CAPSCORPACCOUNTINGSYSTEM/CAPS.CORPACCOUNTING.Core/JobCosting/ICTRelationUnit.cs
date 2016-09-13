using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    [Table("CAPS_ICTRelation")]
    public class ICTRelationUnit : FullAuditedEntity<int>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Declaration of Properties
        /// <summary>Overriding the Id column with ICTRelationId</summary>
        [Column("ICTRelationId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the ICTOrganizationUnitId field. </summary>
        [Required]
        [Range(0, Int64.MaxValue)]
        public long ICTOrganizationUnitId { get; set; }

        [ForeignKey("ICTOrganizationUnitId")]
        public OrganizationUnit Organization { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        [Required]
        [Range(0, Int32.MaxValue)]
        public virtual int JobId { get; set; }

        [ForeignKey("JobId")]
        public JobUnit Job { get; set; }

        /// <summary>Gets or sets the ARClrAccountId field. </summary>
        [Range(0, Int64.MaxValue)]
        public virtual long? ARClrAccountId { get; set; }

        [ForeignKey("ARClrAccountId")]
        public AccountUnit ARClrAccount { get; set; }

        /// <summary>Gets or sets the APClrAccountId field. </summary>
        [Range(0, Int64.MaxValue)]
        public virtual long? APClrAccountId { get; set; }

        [ForeignKey("APClrAccountId")]
        public AccountUnit APClrAccount { get; set; }

        /// <summary>Gets or sets the SubAccountId field. </summary>
        [Range(0, Int64.MaxValue)]
        public virtual long? SubAccountId { get; set; }

        [ForeignKey("SubAccountId")]
        public SubAccountUnit SubAccount { get; set; }


        /// <summary>Gets or sets the LocationId field. </summary>
        public int? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public LocationSetUnit LocationSet { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public int? VendorId { get; set; }

        [ForeignKey("VendorId")]
        public VendorUnit Vendor { get; set; }

        /// <summary>Gets or sets the CustomerId field. </summary>
        public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public CustomerUnit Customer { get; set; }

        /// <summary>Gets or sets the ARBillingTypeId field. </summary>
        [Range(0, Int32.MaxValue)]
        public virtual int? ARBillingTypeId { get; set; }

        [ForeignKey("ARBillingTypeId")]
        public ARBillingTypeUnit ARBillingType { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }
        #endregion

        public ICTRelationUnit() { }
        public ICTRelationUnit(long ictorganizationunitid, int jobid, int? arclraccountid, int? apclraccountid, int? subaccountid, int? locationid, int? vendorid, int? customerid, int? arbillingtypeid, bool isactive, long? organizationunitid)
        {
            ICTOrganizationUnitId = ictorganizationunitid;
            JobId = jobid;
            ARClrAccountId = arclraccountid;
            APClrAccountId = apclraccountid;
            SubAccountId = subaccountid;
            LocationId = locationid;
            VendorId = vendorid;
            CustomerId = customerid;
            ARBillingTypeId = arbillingtypeid;
            IsActive = isactive;
            OrganizationUnitId = organizationunitid;
        }
    }
}
