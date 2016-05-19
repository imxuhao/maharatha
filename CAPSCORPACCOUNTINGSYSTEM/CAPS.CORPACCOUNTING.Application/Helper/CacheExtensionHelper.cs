using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Runtime.Caching;
using CAPS.CORPACCOUNTING.Helpers;

namespace CAPS.CORPACCOUNTING.Helpers
{
    public static class CacheExtensionHelper
    {
        public static ITypedCache<string, DivisionCacheItem> GetDivisionsCache(this ICacheManager cacheManager)
        {
            return cacheManager.GetCache<string, DivisionCacheItem>(DivisionCacheItem.CacheStoreName);
        }
    }
}
