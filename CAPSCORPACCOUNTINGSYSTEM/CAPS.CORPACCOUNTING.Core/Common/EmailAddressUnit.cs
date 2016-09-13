using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CAPS.CORPACCOUNTING.Common
{

    /// <summary>
    ///  EmailAddress is the table name in Lajit
    /// </summary>
    [Table("CAPS_EmailAddress")]
   public class EmailAddressUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        public const int MaxEmailAddressLength = 200;

        /// <summary> Overriding the ID column with EmailAddressId field. </summary>
        [Column("EmailAddressId")]
        public override long Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual long? LajitId { get; set; }

        /// <summary>Gets or sets the EmailAddress field. </summary>
        [Required]
        [StringLength(MaxEmailAddressLength)]
        public virtual string EmailAddress { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

    }
}
