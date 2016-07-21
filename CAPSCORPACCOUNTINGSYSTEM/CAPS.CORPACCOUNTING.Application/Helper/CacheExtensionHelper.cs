using System.Collections.Generic;
using Abp.Configuration;
using Abp.Runtime.Caching;
//using CAPS.CORPACCOUNTING.Configuration.Organization;

namespace CAPS.CORPACCOUNTING.Helpers
{
    public static class CacheExtensionHelper
    {
        public static ITypedCache<string, CacheItem> GetCacheItem(this ICacheManager cacheManager,string CacheStoreName)
        {
            return cacheManager.GetCache<string, CacheItem>(CacheStoreName);
        }
        //public static ITypedCache<string, Dictionary<string, SettingInfoExtended>> GetOrganizationSettingsCache(this ICacheManager cacheManager)
        //{
        //    return cacheManager
        //        .GetCache<string, Dictionary<string, SettingInfoExtended>>(OrganizationSettingManager.OrganizationSettings);
        //}


    }
}
