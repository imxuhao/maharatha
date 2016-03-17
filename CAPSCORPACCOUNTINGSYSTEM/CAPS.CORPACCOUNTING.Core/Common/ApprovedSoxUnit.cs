using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System;

namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    /// ApprovedSOX is the Table name in Lajit
    /// </summary>
    [Table("CAPS_ApprovedSOX")]
    public class ApprovedSoxUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Declaration of Properties
        /// <summary>Overriding the ID column with ApprovedSOXID</summary>
        [Column("ApprovedSOXID")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual long? LajitId { get; set; }

        /// <summary>Gets or sets the TypeOfObjectID field. </summary>
        public virtual TypeofObject TypeOfObjectId { get; set; }
        /// <summary>Gets or sets the ObjectID field. </summary>
        public virtual long ObjectId { get; set; }
        /// <summary>Gets or sets the DateApproved field. </summary>

        [Column(TypeName = "smalldatetime")]
        public virtual DateTime DateApproved { get; set; }
        /// <summary>Gets or sets the ApprovedByUserID field. </summary>
        public virtual int ApprovedByUserId { get; set; }
        /// <summary>Gets or sets the IsUnApproved field. </summary>
        public virtual bool IsUnApproved { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        #endregion
        public ApprovedSoxUnit()
        {
            DateApproved = System.DateTime.Now;
            IsUnApproved = false;
        }
    }
}
