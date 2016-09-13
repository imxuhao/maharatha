using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Security.Dto
{
    /// <summary>
    /// 
    /// </summary>
   public class GetUserSecuritySettingsInputUnit
    {
        /// <summary>Gets or sets the ChartOfAccountId </summary>
        public int? ChartOfAccountId { get; set; }

        /// <summary>Gets or sets the UserId </summary>
        public long UserId { get; set; }

        public List<Filters> Filters { get; set; }

        public EntityClassification EntityClassificationId { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
    }
}
