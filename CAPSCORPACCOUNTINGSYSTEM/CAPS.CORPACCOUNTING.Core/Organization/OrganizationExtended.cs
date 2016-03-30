using System.ComponentModel.DataAnnotations.Schema;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.Organization
{
    [Table("CAPS_OrganizationUnits")]
    public class OrganizationExtended : OrganizationUnit
    {
        public virtual int? LajitId { get; set; }
    }
}
