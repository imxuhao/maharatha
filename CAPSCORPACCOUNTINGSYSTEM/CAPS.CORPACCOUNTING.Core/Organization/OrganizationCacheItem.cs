using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Organization
{
    [Serializable]
    public class OrganizationCacheItem
    {
        public const string CacheName = "AppOrganizationCache";

        public long OrganizationId { get; set; }

        /// <summary>
        /// This will return the date format to applied every where 
        /// </summary>
        public string OrgDateFormat { get; set; }

        public string TenantDateFormat { get; set; }

        /// <summary>
        /// When you pass this id to TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfoId) it provides a TimeZoneInfo
        /// which will be then passed 
        /// </summary>
        public string OrgTimeZoneInfoId { get; set; }

        public string TenantTimeZoneInfoId { get; set; }

        public OrganizationCacheItem()
        { }


        public OrganizationCacheItem(long organizationId, string orgdateformat, string tenantDateFormat, string orgTimeZoneInfoId, string tenantTimeZoneInfoId)
        {

            OrganizationId = organizationId;
            OrgDateFormat = orgdateformat;
            TenantDateFormat = tenantDateFormat;
            OrgTimeZoneInfoId = orgTimeZoneInfoId;
            TenantTimeZoneInfoId = tenantTimeZoneInfoId;


        }


    }
}
