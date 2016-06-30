using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Configuration;
using CAPS.CORPACCOUNTING.Settings;

namespace CAPS.CORPACCOUNTING.Configuration.Organization
{
    /// <summary>
    /// Represents a setting information.
    /// </summary>
    [AutoMapFrom(typeof(SettingExtended))]
    public  class SettingInfoExtended: SettingInfo
    {
        /// <summary>
        /// Gets sets OrganizationunitId
        /// </summary>
        public long OrganizationUnitId { get; set; }

        public SettingInfoExtended(int? tenantId, long? userId, string name, string value, long organizationUnitId ):base(tenantId:tenantId,userId:userId,name:name,value:value)
        {
            OrganizationUnitId = organizationUnitId;

        }

        public SettingInfoExtended()
        {

        }
    }
}
