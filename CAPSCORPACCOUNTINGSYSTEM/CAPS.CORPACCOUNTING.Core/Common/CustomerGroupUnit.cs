using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.Common
{
    /// <summary>
    /// CustomerGroup is the Table name in Lajit
    /// </summary>
    [Table("CAPS_CustomerGroup")]
    public class CustomerGroupUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxDescLength = 50;

        #region Declaration of Properties
        /// <summary>Overriding the ID column with CustomerGroupId</summary>
        [Column("CustomerGroupId")]
        public override int Id { get; set; }
        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescLength)]
        public virtual string Description { get; set; }
        /// <summary>Gets or sets the EntityID field. </summary>
        public virtual int EntityId { get; set; }

        [ForeignKey("EntityId")]
        public virtual EntityUnit Entity { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
        #endregion
    }
}
