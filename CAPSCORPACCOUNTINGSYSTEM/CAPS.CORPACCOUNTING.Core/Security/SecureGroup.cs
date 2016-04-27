using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Security
{

    [Table("CAPS_SecureGroup")]
    public class SecureGroup:FullAuditedEntity,IMayHaveOrganizationUnit,IMustHaveTenant
    {

        [Column("SecureGroupID")]
        public override int Id { get; set; }


        public string SecureGroupName { get; set; }

        public  string Description { get; set; }

        public bool IsActive { get; set; }

        public virtual long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }
    }
}
