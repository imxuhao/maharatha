using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Organizations;


namespace CAPS.CORPACCOUNTING.ExtendedEntities
{
    [Table("CAPS_OrganizationUnits")]
    public class OrganizationExtended : OrganizationUnit
    {
        public virtual int? LajitId { get; set; }
    }
}
