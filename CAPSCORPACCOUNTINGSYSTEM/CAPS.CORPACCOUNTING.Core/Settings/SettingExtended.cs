using Abp.Configuration;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Settings
{
    [Table("CAPS_Settings")]
   public class SettingExtended: Setting, IMayHaveOrganizationUnit
    {
        public virtual long? OrganizationUnitId { get; set; }
    }
}
