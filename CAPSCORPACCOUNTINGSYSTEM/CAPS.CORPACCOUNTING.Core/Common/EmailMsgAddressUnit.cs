using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    ///  EmailMsgAddress is the table name in Lajit
    /// </summary>
    [Table("CAPS_EmailMsgAddress")]
  public class EmailMsgAddressUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary> Overriding the ID column with EmailMsgAddressId field. </summary>
        [Column("EmailMsgAddressId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual long? LajitId { get; set; }

        /// <summary>Gets or sets the EmailMsgLogId field. </summary>
        public virtual long? EmailMsgLogId { get; set; }
        [ForeignKey("EmailMsgLogId")]
        public virtual EmailMsgLogUnit EmailMsgLogUnit { get; set; }

        /// <summary>Gets or sets the EmailAddressId field. </summary>
        public virtual long? EmailAddressId { get; set; }
        [ForeignKey("EmailAddressId")]
        public virtual EmailAddressUnit EmailAddressUnit { get; set; }

        /// <summary>Gets or sets the IsToEmail field. </summary>
        public virtual bool? IsToEmail { get; set; }

        /// <summary>Gets or sets the IsCcEmail field. </summary>
        public virtual bool? IsCcEmail { get; set; }

        /// <summary>Gets or sets the IsBccEmail field. </summary>
        public virtual bool? IsBccEmail { get; set; }

        /// <summary>Gets or sets the IsFromEmail field. </summary>
        public virtual bool? IsFromEmail { get; set; }

        /// <summary>Gets or sets the IsMailDelivered field. </summary>
        public virtual bool? IsMailDelivered { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
